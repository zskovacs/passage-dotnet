using Passage.Configuration;
using Passage.Exceptions;

namespace Passage;

/// <summary>
/// Passage class is used for validating authentication tokens using JWT.
/// </summary>
public class Passage : IPassage
{
    private readonly string _appId;
    private readonly string _appKey = string.Empty;
    private readonly AuthStrategy _authStrategy;
    private JsonWebKeySet _jwks;


    /// <summary>
    /// Passage class constructor
    /// </summary>
    /// <param name="config">Configuration for Passage</param>
    /// <exception cref="PassageException"></exception>
    public Passage(PassageConfig config)
    {
        if (string.IsNullOrEmpty(config?.AppId))
        {
            throw new PassageException("A Passage appID is required. Please include {AppID: YOUR_APP_ID}.");
        }
        _appId = config.AppId;
        
        if (!string.IsNullOrEmpty(config?.AppKey)) {
            _appKey = config.AppKey;
        }

        _authStrategy = config.AuthStrategy;
    }
    
    /// <summary>
    /// Determine if the provided token is valid when compared with its respective public key.
    /// </summary>
    /// <param name="token">Authentication token</param>
    /// <returns>SUB claim if JWT can be verified</returns>
    /// <exception cref="PassageException"></exception>
    public async Task<string> ValidateToken(string token)
    {
        _jwks ??= await DownloadJWKS();
        
        var kid = GetKidFromJwtToken(token);
        var key = _jwks.GetSigningKeys().SingleOrDefault(k => k.KeyId == kid);

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            RequireSignedTokens = true,
            ValidateIssuer = true,
            ValidIssuer = $"https://auth.passage.id/v1/apps/{_appId}",
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKeys = new[] { key }
        };
        
        try
        {
            var result = await tokenHandler.ValidateTokenAsync(token, validationParameters);
            return result.ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        catch (Exception ex)
        {
            throw new PassageException("JWT verification failed", ex);
        }
    }

    private static string GetKidFromJwtToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        return jsonToken?.Header?.Kid;
    }
    
    private async Task<JsonWebKeySet> DownloadJWKS()
    {
        using var httpClient = new HttpClient();
        var jwks = await httpClient.GetStringAsync($"https://auth.passage.id/v1/apps/{_appId}/.well-known/jwks.json");

        if (string.IsNullOrEmpty(jwks))
            throw new PassageException("Cannot download JWKS");

        return new JsonWebKeySet(jwks);
    }
}