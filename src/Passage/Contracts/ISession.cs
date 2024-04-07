namespace Passage.Contracts;

/// <summary>
/// Represents a user session with Passage.
/// </summary>
public interface ISession
{
    /// <summary>
    /// Determine if the provided token is valid when compared with its respective public key.
    /// </summary>
    /// <param name="token">Authentication token</param>
    /// <returns>SUB claim if JWT can be verified</returns>
    /// <exception cref="PassageException"></exception>
    Task<string> ValidateToken(string token);

    /// <summary>
    /// Revokes the refresh token of a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="PassageException">
    /// Thrown when there is an error revoking the refresh token.</exception>
    Task RevokeRefreshToken(string userId, CancellationToken cancellationToken = default);
}