namespace Passage;

/// <summary>
/// Passage class is used for validating authentication tokens using JWT.
/// </summary>
public class Passage : IPassage
{
    private readonly string _appId;
    private readonly string _apiKey = string.Empty;
    private readonly AuthStrategy _authStrategy;
    private JsonWebKeySet _jwks;
    protected HttpClient _httpClient;

    /// <summary>
    /// Passage class constructor
    /// </summary>
    /// <param name="config">Configuration for Passage</param>
    /// <exception cref="PassageException"></exception>
    public Passage(PassageConfig config)
    {
        if (string.IsNullOrEmpty(config?.AppId))
        {
            throw new PassageException(Errors.Config.MissingAppId);
        }

        _appId = config.AppId;

        if (!string.IsNullOrEmpty(config?.ApiKey))
        {
            _apiKey = config.ApiKey;
        }

        _authStrategy = config.AuthStrategy;
    }

    /// <summary>
    /// Passage class constructor
    /// </summary>
    /// <param name="config">Configuration for Passage</param>
    /// <param name="httpClient">Http Client for overload</param>
    /// <exception cref="PassageException"></exception>
    public Passage(PassageConfig config, HttpClient httpClient) : this(config)
    {
        _httpClient = httpClient;
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
            throw new PassageException(Errors.Token.VerificationFailed, ex);
        }
    }

    /// <summary>
    /// Get information about an application.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="Errors.App"/> object</returns>
    /// <exception cref="PassageException"></exception>
    public async Task<AppInfo> GetApp(CancellationToken cancellationToken = default)
    {
        try
        {
            var client = new PassageClient(GetHttpClient());
            var result = await client.GetAppAsync(_appId, cancellationToken);
            return result.App;
        }
        catch (ApiException ex)
        {
            throw new PassageException(Errors.App.CannotGet, ex);
        }
        catch (PassageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new PassageException(Errors.App.CannotGet, ex);
        }
    }

    /// <summary>
    /// Get user information, if the user exists. This endpoint can be used to determine whether a user has an existing account and if they should login or register.
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    public async Task<UserInfo> GetUser(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = new PassageClient(GetHttpClient());
            var result = await client.GetUserAsync(_appId, userId, cancellationToken);
            return result.User;
        }
        catch (ApiException ex)
        {
            throw new PassageException(Errors.User.CannotGet, ex);
        }
        catch (PassageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new PassageException(Errors.User.CannotGet, ex);
        }
    }

    /// <summary>
    /// Get user information, if the user exists. This endpoint can be used to determine whether a user has an existing account and if they should login or register.
    /// </summary>
    /// <param name="identifier">Email or Phone Number</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    public async Task<UserInfo> GetUserByIdentifier(string identifier, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = new PassageClient(GetHttpClient());
            var result = await client.ListPaginatedUsersAsync(
                page: null,
                limit: 1,
                created_before: null,
                order_by: null,
                identifier: identifier,
                id: null,
                login_count: null,
                status: null,
                created_at: null,
                updated_at: null,
                last_login_at: null,
                app_id: _appId,
                cancellationToken: cancellationToken);

            if (result.Users.Count == 1)
            {
                return await GetUser(result.Users.First().Id, cancellationToken);
            }

            throw new PassageException(Errors.User.IdentifierNotFound);
        }
        catch (ApiException ex)
        {
            throw new PassageException(Errors.User.CannotGet, ex);
        }
        catch (PassageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new PassageException(Errors.User.CannotGet, ex);
        }
    }


    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="createUserRequest">Payload to create the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    public async Task<UserInfo> CreateUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = new PassageClient(GetHttpClient());
            var result = await client.CreateUserAsync(createUserRequest, _appId, cancellationToken);
            return result.User;
        }
        catch (ApiException ex)
        {
            throw new PassageException(Errors.User.CannotCreate, ex);
        }
        catch (PassageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new PassageException(Errors.User.CannotCreate, ex);
        }
    }

    /// <summary>
    /// Delete a user.
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="PassageException"></exception>
    public async Task DeleteUser(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = new PassageClient(GetHttpClient());
            await client.DeleteUserAsync(_appId, userId, cancellationToken);
        }
        catch (ApiException ex)
        {
            throw new PassageException(Errors.User.CannotDelete, ex);
        }
        catch (PassageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new PassageException(Errors.User.CannotDelete, ex);
        }
    }


    /// <summary>
    /// Update a User
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="updateUserRequest">Payload to update the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    public async Task<UserInfo> UpdateUser(string userId, UpdateUserRequest updateUserRequest, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = new PassageClient(GetHttpClient());
            var result = await client.UpdateUserAsync(updateUserRequest, _appId, userId, cancellationToken);
            return result.User;
        }
        catch (ApiException ex)
        {
            throw new PassageException(Errors.User.CannotUpdate, ex);
        }
        catch (PassageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new PassageException(Errors.User.CannotUpdate, ex);
        }
    }

    private HttpClient GetHttpClient()
    {
        if (_httpClient is not null)
        {
            return _httpClient;
        }
        
        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new PassageException(Errors.Config.MissingApiKey);
        }
        
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        _httpClient.DefaultRequestHeaders.Add("Passage-Version", Assembly.GetExecutingAssembly().GetName().Version.ToString());

        return _httpClient;
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
            throw new PassageException(Errors.Token.CannotDownloadJwks);

        return new JsonWebKeySet(jwks);
    }
}