using Unhosted_Device_side.Data;
using Unhosted_Device_side.Data.Tables;

namespace Unhosted_Device_side.Services;

public class RentService
{
    private readonly AppDatabase _db;

    public RentService(AppDatabase db)
    {
        _db = db;
    }

    public async Task<string> RentGameAsync(Guid userId, Guid gameId)
    {
        var games = await _db.GetGamesAsync();
        var game = games.FirstOrDefault(g => g.Id == gameId);

        if (game is null)
            return "Game not found.";

        if (game.Quantity <= 0)
            return $"No copies of {game.Name} available.";

        var Renting = await _db.GetRentsAsync();
        
        var CurRenting = Renting.Any(r =>
            r.UserId == userId &&
            r.GameId == gameId &&
            r.Active);

        if (CurRenting)
            return $"You already rent {game.Name}.";

        var rent = new RentEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            GameId = gameId,
            Active = true
        };

        game.Quantity--;

        await _db.SaveRentAsync(rent);
        await _db.SaveGameAsync(game);

        return $"You rented {game.Name}!";
    }
}