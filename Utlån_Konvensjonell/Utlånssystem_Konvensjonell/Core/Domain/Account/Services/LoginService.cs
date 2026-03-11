using Utlånssystem_Konvensjonell.Core.Domain.Account.Events;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Handlers;
namespace Utlånssystem_Konvensjonell.Core.Domain.Account.Services;
public class LoginService
{
    private readonly LoginUserHandler _handler;

    public LoginService(LoginUserHandler handler)
    {
        _handler = handler;
    }

    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        return await _handler.ValidateAsync(email, password);
    }
}