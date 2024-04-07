namespace Passage.Contracts;

/// <summary>
/// Interface for managing user-related operations.
/// </summary>
public interface IUser
{
    /// <summary>
    /// Get user information, if the user exists. This endpoint can be used to determine whether a user has an existing account and if they should login or register.
    /// </summary>
    /// <param name="identifier">Email or Phone Number</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    Task<UserInfo> GetByIdentifier(string identifier, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user information, if the user exists. This endpoint can be used to determine whether a user has an existing account and if they should login or register.
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    Task<UserInfo> Get(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="createUserRequest">Payload to create the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="User"/> object</returns>
    /// <exception cref="PassageException"></exception>
    Task<UserInfo> Create(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a user.
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="PassageException"></exception>
    Task Delete(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a User
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="updateUserRequest">Payload to update the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    Task<UserInfo> Update(string userId, UpdateUserRequest updateUserRequest, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Activates a user in the Passage system.
    /// </summary>
    /// <param name="userId">The ID of the user to activate.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
    /// <returns>The activated user information.</returns>
    /// <exception cref="PassageException">Thrown when there is an error activating the user.</exception>
    Task<UserInfo> Activate(string userId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deactivates a user.
    /// </summary>
    /// <param name="userId">The ID of the user to deactivate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deactivated user information.</returns>
    /// <exception cref="PassageException">Thrown when there is an error deactivating the user.</exception>
    Task<UserInfo> Deactivate(string userId, CancellationToken cancellationToken = default);
}