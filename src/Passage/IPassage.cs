﻿namespace Passage;

/// <summary>
/// Interface for Passage class
/// </summary>
public interface IPassage
{
    /// <summary>
    /// Determine if the provided token is valid when compared with its respective public key.
    /// </summary>
    /// <param name="token">Authentication token</param>
    /// <returns>SUB claim if JWT can be verified</returns>
    /// <exception cref="PassageException"></exception>
    Task<string> ValidateToken(string token);

    /// <summary>
    /// Get App Info about an app
    /// </summary>
    /// <returns>Passage App object</returns>
    Task<App> GetApp(CancellationToken cancellationToken = default);
}