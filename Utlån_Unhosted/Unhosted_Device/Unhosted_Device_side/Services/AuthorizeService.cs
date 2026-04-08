using Unhosted_Device_side.Data;
using Unhosted_Device_side.Data.Tables;
using Unhosted_Device_side.Models;

namespace Unhosted_Device_side.Services;

public class AuthorizeService
{
    private readonly AppDatabase _database;
    private readonly AuthorizeServiceAPI _authorizeServiceAPI;

    public AuthorizeService(AppDatabase database, AuthorizeServiceAPI authorizeServiceAPI)
    {
        _database = database;
        _authorizeServiceAPI = authorizeServiceAPI;
    }

    public async Task<bool> SyncAuthorizationAsync(Guid userId)
    {
        var localUser = await _database.GetUserAsync(userId);
        if (localUser is null)
            return false;

        var isAuthorized = await _authorizeServiceAPI.GetIsAuthorizedAsync(userId);

        if (isAuthorized is null)
            return false;

        localUser.IsAuthorized = isAuthorized.Value;
        await _database.SaveUserAsync(localUser);

        return true;
    }

    public async Task<List<UnauthorUsers>> GetUnauthorizedUsersAsync()
    {
        return await _authorizeServiceAPI.GetUnauthorizedUsersAsync()
            ?? new List<UnauthorUsers>();
    }

    public async Task<bool> AuthorizeUserAsync(Guid userId)
    {
        return await _authorizeServiceAPI.AuthorizeUserAsync(userId);
    }
}