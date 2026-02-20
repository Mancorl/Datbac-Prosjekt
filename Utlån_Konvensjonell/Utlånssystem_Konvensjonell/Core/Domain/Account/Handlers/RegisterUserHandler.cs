using System;
using System.Threading.Tasks;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.Core.Domain.Account;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Events;

namespace Utlånssystem_Konvensjonell.Core.Domain.Account.Handlers
{
    public class RegisterUserHandler
    {
        private readonly BoardGameContext _db;

        public RegisterUserHandler(BoardGameContext db)
        {
            _db = db;
        }

        public void OnRegistered(object? sender, RegisteredEventArgs e)
{
    _ = HandleAsync(e);
}

private async Task HandleAsync(RegisteredEventArgs e)
{
    var user = new User(
        e.Email,
        e.Password,
        e.FirstName,
        e.LastName
    );

    _db.Users.Add(user);
    await _db.SaveChangesAsync();
}
    }
}