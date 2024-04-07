namespace Passage;

/// <summary>
/// Passage class is used for validating authentication tokens using JWT.
/// </summary>
public class Passage : IPassage
{
    public IApp App { get; }
    public IUser User { get; }
    public ISession Session { get; }

    /// <summary>
    /// Passage class constructor
    /// </summary>
    /// <param name="config">Configuration for Passage</param>
    /// <exception cref="PassageException"></exception>
    public Passage(PassageConfig config)
    {
        App = new App(config);
        User = new User(config);
        Session = new Session(config);
    }

    /// <summary>
    /// Passage class constructor
    /// </summary>
    /// <param name="config">Configuration for Passage</param>
    /// <param name="httpClient">Http Client for overload</param>
    /// <exception cref="PassageException"></exception>
    public Passage(PassageConfig config, HttpClient httpClient)
    {
        App = new App(config, httpClient);
        User = new User(config, httpClient);
        Session = new Session(config, httpClient);
    }
}