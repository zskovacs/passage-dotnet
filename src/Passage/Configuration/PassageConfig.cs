namespace Passage.Configuration;

/// <summary>
/// Represents the configuration for the Passage authentication library.
/// </summary>
public class PassageConfig
{
    /// <summary>
    /// Application Id for Passage (see: https://passage.id)
    /// </summary>
    public string AppId { get; set; }
    
    /// <summary>
    /// API key for access Passage from your application
    /// </summary>
    public string AppKey { get; set; }

    /// <summary>
    /// Represents the authentication strategy for Passage.
    /// </summary>
    public AuthStrategy AuthStrategy { get; set; }
}