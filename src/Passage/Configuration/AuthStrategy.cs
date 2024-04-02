namespace Passage.Configuration;

/// <summary>
/// Represents the authentication strategy for Passage.
/// </summary>
public enum AuthStrategy
{
    /// <summary>
    /// Represents the authentication strategy of using cookies for authentication.
    /// </summary>
    Cookie,

    /// <summary>
    /// Represents the authentication strategy of using local storage/headers for Passage.
    /// </summary>
    Header
}