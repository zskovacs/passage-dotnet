namespace Passage;

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
    /// <returns>A Passage <see cref="App"/> object</returns>
    /// <exception cref="PassageException"></exception>
    Task<AppInfo> GetApp(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user information, if the user exists. This endpoint can be used to determine whether a user has an existing account and if they should login or register.
    /// </summary>
    /// <param name="identifier">Email or Phone Number</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    Task<UserInfo> GetUserByIdentifier(string identifier, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user information, if the user exists. This endpoint can be used to determine whether a user has an existing account and if they should login or register.
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    Task<UserInfo> GetUser(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="createUserRequest">Payload to create the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="User"/> object</returns>
    /// <exception cref="PassageException"></exception>
    Task<UserInfo> CreateUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a user.
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="PassageException"></exception>
    Task DeleteUser(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a User
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="updateUserRequest">Payload to update the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    Task<UserInfo> UpdateUser(string userId, UpdateUserRequest updateUserRequest, CancellationToken cancellationToken = default);
}