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

        public static async Task<string> OnRegistered(object? sender, RegisteredEventArgs e)
        {
            var user = new User(
                e.Email,
                e.Password,
                e.FirstName,
                e.LastName
            );
            try
            {
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
                return "User registered successfully.";
            }
            catch (Exception ex)
            {
                return $"An error occurred during registration: {ex.Message}";
            }
        }
    }
}