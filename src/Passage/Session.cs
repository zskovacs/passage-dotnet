namespace Passage;

/// <summary>
/// Represents a user session with Passage.
/// </summary>
public class Session: BaseClient, ISession
{
    private JsonWebKeySet _jwks;
    
    /// <summary>
    /// Represents a user session with Passage.
    /// </summary>
    /// <param name="passageConfig"></param>
    /// <param name="httpClient"></param>
    public Session(PassageConfig passageConfig, HttpClient httpClient) : base(passageConfig, httpClient) 
    {
        
    }

    /// <summary>
    /// Represents a user session with Passage.
    /// </summary>
    /// <param name="passageConfig"></param>
    public Session(PassageConfig passageConfig) : base(passageConfig)
    {
        
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
            ValidIssuer = $"https://auth.passage.id/v1/apps/{AppId}",
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
            throw new PassageException(Errors.Token.VerificationFailed, ex);
        }
    }

    /// <summary>
    /// Revokes the refresh token of a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="PassageException">
    /// Thrown when there is an error revoking the refresh token.</exception>
    public async Task RevokeRefreshToken(string userId, CancellationToken cancellationToken = default)
    {
        await CallClientMethod(client => client.RevokeUserRefreshTokensAsync(AppId, userId, cancellationToken));
    }
    
    
    private static string GetKidFromJwtToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        return jsonToken?.Header?.Kid;
    }

    private async Task<JsonWebKeySet> DownloadJWKS()
    {
        var jwks = await GetHttpClient().GetStringAsync($"https://auth.passage.id/v1/apps/{AppId}/.well-known/jwks.json");

        if (string.IsNullOrEmpty(jwks))
            throw new PassageException(Errors.Token.CannotDownloadJwks);

        return new JsonWebKeySet(jwks);
    }
}