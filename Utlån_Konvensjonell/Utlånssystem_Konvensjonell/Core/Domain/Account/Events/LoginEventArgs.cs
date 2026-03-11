using System;

namespace Utlånssystem_Konvensjonell.Core.Domain.Account.Events;

public class LoginEventArgs : EventArgs
{
    public LoginEventArgs(Guid userId, string email)
    {
        UserId = userId;
        Email = email;
    }

    public Guid UserId { get; }
    public string Email { get; }
}