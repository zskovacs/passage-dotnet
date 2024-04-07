namespace Passage;

/// <summary>
/// Represents a User in the Passage system.
/// </summary>
public class User : BaseClient, IUser
{
    /// <summary>
    /// Represents a User in the Passage system.
    /// </summary>
    /// <param name="passageConfig"></param>
    /// <param name="httpClient"></param>
    public User(PassageConfig passageConfig, HttpClient httpClient) : base(passageConfig, httpClient)
    {
    }

    /// <summary>
    /// Represents a User in the Passage system.
    /// </summary>
    /// <param name="passageConfig"></param>
    public User(PassageConfig passageConfig) : base(passageConfig)
    {
    }

    /// <summary>
    /// Get user information, if the user exists. This endpoint can be used to determine whether a user has an existing account and if they should login or register.
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    public async Task<UserInfo> Get(string userId, CancellationToken cancellationToken = default)
    {
        var result = await CallClientMethod(client => client.GetUserAsync(AppId, userId, cancellationToken));
        return result.User;
    }

    /// <summary>
    /// Get user information, if the user exists. This endpoint can be used to determine whether a user has an existing account and if they should login or register.
    /// </summary>
    /// <param name="identifier">Email or Phone Number</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    public async Task<UserInfo> GetByIdentifier(string identifier, CancellationToken cancellationToken = default)
    {
        var result = await CallClientMethod(client => client.ListPaginatedUsersAsync(
            page: null,
            limit: 1,
            created_before: null,
            order_by: null,
            identifier: identifier,
            id: null,
            login_count: null,
            status: null,
            created_at: null,
            updated_at: null,
            last_login_at: null,
            app_id: AppId,
            cancellationToken: cancellationToken));

        if (result.Users.Count == 1)
        {
            return await Get(result.Users.First().Id, cancellationToken);
        }

        throw new PassageException(Errors.User.IdentifierNotFound);
    }


    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="createUserRequest">Payload to create the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    public async Task<UserInfo> Create(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default)
    {
        var result = await CallClientMethod(client => client.CreateUserAsync(createUserRequest, AppId, cancellationToken));
        return result.User;
    }

    /// <summary>
    /// Delete a user.
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="PassageException"></exception>
    public async Task Delete(string userId, CancellationToken cancellationToken = default)
    {
        await CallClientMethod(client => client.DeleteUserAsync(AppId, userId, cancellationToken));
    }


    /// <summary>
    /// Update a User
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="updateUserRequest">Payload to update the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A Passage <see cref="UserInfo"/> object</returns>
    /// <exception cref="PassageException"></exception>
    public async Task<UserInfo> Update(string userId, UpdateUserRequest updateUserRequest, CancellationToken cancellationToken = default)
    {
        var result = await CallClientMethod(client => client.UpdateUserAsync(updateUserRequest, AppId, userId, cancellationToken));
        return result.User;
    }

    /// <summary>
    /// Activates a user in the Passage system.
    /// </summary>
    /// <param name="userId">The ID of the user to activate.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
    /// <returns>The activated user information.</returns>
    /// <exception cref="PassageException">Thrown when there is an error activating the user.</exception>
    public async Task<UserInfo> Activate(string userId, CancellationToken cancellationToken = default)
    {
        var result = await CallClientMethod(client => client.ActivateUserAsync(AppId, userId, cancellationToken));
        return result.User;
    }

    /// <summary>
    /// Deactivates a user.
    /// </summary>
    /// <param name="userId">The ID of the user to deactivate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deactivated user information.</returns>
    /// <exception cref="PassageException">Thrown when there is an error deactivating the user.</exception>
    public async Task<UserInfo> Deactivate(string userId, CancellationToken cancellationToken = default)
    {
        var result = await CallClientMethod(client => client.DeactivateUserAsync(AppId, userId, cancellationToken));
        return result.User;
    }
}