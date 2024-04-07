namespace Passage;

/// <summary>
/// Interface for Passage class
/// </summary>
public interface IPassage
{
    /// <summary>
    /// Represents an application in the Passage authentication system.
    /// </summary>
    public IApp App { get; }

    /// <summary>
    /// Represents a user in the Passage system.
    /// </summary>
    public IUser User { get; }

    /// <summary>
    /// Represents thes current session in the Passage system.
    /// </summary>
    public ISession Session { get; }
}