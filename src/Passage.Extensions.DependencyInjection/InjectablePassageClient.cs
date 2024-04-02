namespace Passage.Extensions.DependencyInjection;

/// <summary>
/// InjectablePassageClient class is a client for the Passage authentication library that can be injected into other classes.
/// </summary>
public class InjectablePassageClient : Passage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InjectablePassageClient"/> class.
    /// InjectablePassageClient class is a client for the Passage authentication library that can be injected into other classes.
    /// </summary>
    /// <param name="options">Passage Configuration.</param>
    /// <param name="httpClient">Injected httpClient.</param>
    public InjectablePassageClient(IOptions<PassageConfig> options, HttpClient httpClient)
        : base(options.Value)
    {
        this._httpClient = httpClient;
        this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Value.ApiKey);
        this._httpClient.DefaultRequestHeaders.Add("Passage-Version", Assembly.GetExecutingAssembly().GetName().Version.ToString());
    }
}