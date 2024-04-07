namespace Passage;

/// <summary>
/// Passage class is used for validating authentication tokens using JWT.
/// </summary>
public class Passage : IPassage
{
    /// <summary>
    /// The <see cref="App"/> class represents an application in the Passage system.
    /// </summary>
    /// <remarks>
    /// This class is used for managing the application details and configurations.
    /// </remarks>
    /// <example>
    /// This example demonstrates how to retrieve the details of an application using the <see cref="App.Get"/> method:
    /// <code>
    /// var app = new App(config);
    /// var appInfo = await app.Get();
    /// Console.WriteLine($"Application Name: {appInfo.Name}");
    /// Console.WriteLine($"Application Description: {appInfo.Description}");
    /// </code>
    /// </example>
    public IApp App { get; }

    /// <summary>
    /// Represents a <see cref="User"/> and provides methods for managing user-related operations.
    /// </summary>
    public IUser User { get; }

    /// <summary>
    /// Represents a user <see cref="Session"/> with Passage.
    /// </summary>
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