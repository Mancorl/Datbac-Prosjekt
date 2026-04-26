using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.Core.Domain.Borrowed;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames;

namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Handlers
{
    public class ReturnHandler
    {
        private readonly BoardGameContext _db;

        public ReturnHandler(BoardGameContext db)
        {
            _db = db;
        }

        public async Task ReturnAsync(Guid userId, Guid borrowId)
        {
            var borrowing = await _db.Rented.FirstOrDefaultAsync(r =>
                r.Id == borrowId &&
                r.UserId == userId &&
                r.Active);

            if (borrowing == null)
                throw new InvalidOperationException("Active borrowing not found.");

            borrowing.Returned();

            await _db.SaveChangesAsync();
        }
    }
}