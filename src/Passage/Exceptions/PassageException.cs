namespace Passage.Exceptions;

/// <summary>
/// General exception for Passage
/// </summary>
public class PassageException : Exception
{
    /// <summary>
    /// HTTP Status Code for exception
    /// </summary>
    public int StatusCode { get; set; } = 500;
    

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    public PassageException(string message) : base(message)
    {
        
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public PassageException(string message, ApiException ex) : base(message, ex)
    {
        StatusCode = ex.StatusCode;
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public PassageException(string message, Exception ex) : base(message, ex)
    {

    }
}