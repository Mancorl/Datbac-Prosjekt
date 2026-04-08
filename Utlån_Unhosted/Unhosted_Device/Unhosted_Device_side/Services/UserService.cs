using Unhosted_Device_side.Data;
using Unhosted_Device_side.Data.Tables;

namespace Unhosted_Device_side.Services;

public class UserService
{
    private readonly UserServiceAPI _userServiceAPI;

    private readonly AppDatabase _database;

    public UserService(AppDatabase database, UserServiceAPI userServiceAPI)
    {
        _userServiceAPI = userServiceAPI;
        _database = database;
    }

    public async Task<(bool Success, string Message)> SendUserAsync(UserEntity user)
{
    var result = await _userServiceAPI.SendUserForAuthorizationAsync(user);

    if (!result.Success)
        return (false, result.Message);

    if (result.User is not null)
    {
        user.IsAuthorized = result.User.IsAuthorized;
        user.Permission = result.User.Permission;

        await _database.SaveUserAsync(user);
    }

    return (true, result.Message);
}
}