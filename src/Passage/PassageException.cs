using System;

namespace Passage;

/// <summary>
/// 
/// </summary>
public class PassageException : Exception
{
    public int StatusCode { get; set; } = 500;

    public PassageException(string message) : base(message)
    {
        
    }
    
}