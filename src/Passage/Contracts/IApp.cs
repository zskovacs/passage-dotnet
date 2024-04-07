namespace Passage.Contracts;

/// <summary>
/// Represents an application interface.
/// </summary>
public interface IApp
{
    /// <summary>
    /// Get App Info about an app
    /// </summary>
    /// <returns>A Passage <see cref="App"/> object</returns>
    /// <exception cref="PassageException"></exception>
    Task<AppInfo> Get(CancellationToken cancellationToken = default);
}