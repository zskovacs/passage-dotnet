namespace Passage;

/// <summary>
/// The abstract base class for Passage clients.
/// </summary>
public abstract class BaseClient
{
    /// <summary>
    /// Represents the Application ID used by the Passage client.
    /// </summary>
    /// <remarks>
    /// The AppId is a unique identifier that identifies a specific application within Passage.
    /// </remarks>
    protected readonly string AppId;
    
    private HttpClient _httpClient;
    private readonly string _apiKey = string.Empty;
    
    /// <summary>
    /// App class constructor
    /// </summary>
    /// <param name="config">Configuration for Passage</param>
    /// <exception cref="PassageException"></exception>
    protected BaseClient(PassageConfig config)
    {
        if (string.IsNullOrEmpty(config?.AppId))
        {
            throw new PassageException(Errors.Config.MissingAppId);
        }

        AppId = config.AppId;

        if (!string.IsNullOrEmpty(config?.ApiKey))
        {
            _apiKey = config.ApiKey;
        }
    }

    /// <summary>
    /// App class constructor
    /// </summary>
    /// <param name="config">Configuration for Passage</param>
    /// <param name="httpClient">Http Client for overload</param>
    /// <exception cref="PassageException"></exception>
    protected BaseClient(PassageConfig config, HttpClient httpClient) : this(config)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets the HttpClient instance for making HTTP requests.
    /// </summary>
    /// <returns>The HttpClient instance.</returns>
    /// <exception cref="PassageException">Thrown when a PassageException occurs.</exception>
    protected HttpClient GetHttpClient()
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

    private PassageClient _passageClient;
    private PassageClient CreateClient() => _passageClient ??= new PassageClient(GetHttpClient());

    /// <summary>
    /// Calls the specified client method asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="clientAction">The client method to call.</param>
    /// <returns>The result of the client method.</returns>
    /// <exception cref="PassageException">Thrown when there is a Passage-related exception.</exception>
    protected async Task<T> CallClientMethod<T>(Func<IPassageClient, Task<T>> clientAction)
    {
        var client = CreateClient();
        try
        {
            var result = await clientAction(client).ConfigureAwait(continueOnCapturedContext: false);
            return result;
        }
        catch (PassageException)
        {
            throw;
        }
        catch (ApiException ex)
        {
            throw new PassageException(Errors.Client.ApiException, ex);
        }
        catch (Exception ex)
        {
            throw new PassageException(Errors.Client.UnexpectedError, ex);
        }
    }

    /// <summary>
    /// Calls the specified client action method on the Passage client.
    /// </summary>
    /// <param name="clientAction">The client action method to be called.</param>
    /// <returns>A task representing the asynchronous client action.</returns>
    /// <exception cref="PassageException">Thrown when an error occurs during the client action.</exception>
    protected async Task CallClientMethod(Func<IPassageClient, Task> clientAction)
    {
        var client = CreateClient();
        try
        {
            await clientAction(client).ConfigureAwait(continueOnCapturedContext: false);
        }
        catch (PassageException)
        {
            throw;
        }
        catch (ApiException ex)
        {
            throw new PassageException(Errors.Client.ApiException, ex);
        }
        catch (Exception ex)
        {
            throw new PassageException(Errors.Client.UnexpectedError, ex);
        }
    }
}