namespace Passage;

/// <summary>
/// Represents an application in the Passage authentication library.
/// </summary>
public class App : BaseClient, IApp
{
    /// <summary>
    /// Represents an application in the Passage authentication library.
    /// </summary>
    /// <param name="passageConfig"></param>
    /// <param name="httpClient"></param>
    public App(PassageConfig passageConfig, HttpClient httpClient) : base(passageConfig, httpClient) 
    {
        
    }

    /// <summary>
    /// Represents an application in the Passage authentication library.
    /// </summary>
    /// <param name="passageConfig"></param>
    public App(PassageConfig passageConfig) : base(passageConfig)
    {
        
    }
    
    /// <summary>
    /// Get App Info about an apps
    /// </summary>
    /// <returns>A Passage <see cref="App"/> object</returns>
    /// <exception cref="PassageException"></exception>
    public async Task<AppInfo> Get(CancellationToken cancellationToken = default)
    {
        var result = await CallClientMethod(client => client.GetAppAsync(AppId, cancellationToken));
        return result.App;
    }
}