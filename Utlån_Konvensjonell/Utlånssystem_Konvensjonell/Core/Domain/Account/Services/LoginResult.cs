using Utlånssystem_Konvensjonell.Core.Domain.Account;

namespace Utlånssystem_Konvensjonell.Core.Domain.Account.Services;

public record LoginResult(bool Success, User? User = null, string? Error = null)
{
    public static LoginResult Ok(User user) => new(true, user, null);

    public static LoginResult Fail(string error) => new(false, null, error);
}