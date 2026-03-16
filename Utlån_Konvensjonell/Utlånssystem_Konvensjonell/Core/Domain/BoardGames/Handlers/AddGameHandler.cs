using System;
using System.Threading.Tasks;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Events;

namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Handlers
{
    public class AddGameHandler
    {
        private readonly BoardGameContext _db;

        public AddGameHandler(BoardGameContext db)
        {
            _db = db;
        }

        public void OnRegistered(object? sender, AddGameEventArgs e)
{
    _ = HandleAsync(e);
}

private async Task HandleAsync(AddGameEventArgs e)
{
    var game = new BoardGame(
        e.GameTitle,
        e.Quantity,
        e.Loanable,
        e.ImagePath
    );

    _db.Games.Add(game);
    await _db.SaveChangesAsync();
}
    }
}