using System;
using System.Threading.Tasks;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.Core.Domain.Account;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Events;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Services;

namespace Utlånssystem_Konvensjonell.Core.Domain.Account.Handlers

{

    public class LoginUserHandler
    {
        private readonly BoardGameContext _db;

        public LoginUserHandler(BoardGameContext db)
        {
            _db = db;
        }

        public async Task<LoginResult> ValidateAsync(string email, string password)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return LoginResult.Fail("User not found");

            if (user.Password != password)
                return LoginResult.Fail("Incorrect password");

            return LoginResult.Ok(user);
        }
    }

}


