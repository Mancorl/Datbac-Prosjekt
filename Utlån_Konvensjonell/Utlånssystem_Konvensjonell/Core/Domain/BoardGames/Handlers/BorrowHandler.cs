using System;
using System.Threading.Tasks;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.Core.Domain.Account;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Events;
using Utlånssystem_Konvensjonell.Core.Domain.Borrowed;

namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Handlers
{
   public class BorrowHandler
{
    private readonly BoardGameContext _db;

    public BorrowHandler(BoardGameContext db)
    {
        _db = db;
    }

    public async Task<string> BorrowAsync(Guid userId, Guid gameId)
    {
        var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            throw new InvalidOperationException("Game not found.");

        game.RentOne();

        var borrowing = new Borrowing(userId, game.Id, true);
        _db.Rented.Add(borrowing);

        await _db.SaveChangesAsync();

        return game.Name;
    }
}
}
