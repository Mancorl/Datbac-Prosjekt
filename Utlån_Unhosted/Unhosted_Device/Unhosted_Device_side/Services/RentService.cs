using Unhosted_Device_side.Data;
using Unhosted_Device_side.Data.Tables;
using Unhosted_Device_side.Services;

namespace Unhosted_Device_side.Services;

public class RentService
{
    private readonly AppDatabase _db;
    private readonly RentServiceAPI _rentServiceAPI;
    private readonly GameService _gameService;

    public RentService(AppDatabase db, RentServiceAPI rentServiceAPI, GameService gameService)
    {
        _db = db;
        _rentServiceAPI = rentServiceAPI;
        _gameService = gameService;
    }

    public async Task<string> RentGameAsync(Guid userId, Guid gameId)
    {
        var result = await _rentServiceAPI.BorrowGameAsync(userId, gameId);

        if (!result.Success)
            return $"Could not rent game: {result.Message}";

        var games = await _db.GetGamesAsync();
        var game = games.FirstOrDefault(g => g.Id == gameId);

        await _gameService.GetGamesFromApiAsync();

        return $"You rented {game.Name}";
    }


    public async Task SyncUserRentsAsync(Guid userId)
{
    var apiRents = await _rentServiceAPI.GetUserRentsAsync(userId);

    if (apiRents is null)
        return;

    
    var localRents = await _db.GetRentsAsync();
    foreach (var r in localRents)
        await _db.DeleteRentAsync(r.Id);

    
    foreach (var rent in apiRents)
    {
        var entity = new RentEntity
        {
            Id = rent.Id,
            UserId = rent.UserId,
            GameId = rent.GameId,
            Active = rent.Active
        };

        await _db.SaveRentAsync(entity);
    }
}
}