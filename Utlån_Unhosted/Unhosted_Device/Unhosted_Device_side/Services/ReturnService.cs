using Unhosted_Device_side.Data;
using Unhosted_Device_side.Data.Tables;

namespace Unhosted_Device_side.Services;

public class ReturnService
{
    private readonly AppDatabase _db;
    private readonly ReturnServiceAPI _returnServiceAPI;
    private readonly GameService _gameService;

    public ReturnService(AppDatabase db, ReturnServiceAPI returnServiceAPI, GameService gameService)
    {
        _db = db;
        _returnServiceAPI = returnServiceAPI;
        _gameService = gameService;
    }

    public async Task<string> ReturnGameAsync(UserEntity user, Guid gameId)
    {
        var result = await _returnServiceAPI.ReturnGameAsync(user.Id, gameId);

        if (!result.Success)
            return $"Could not return game: {result.Message}";

        var rents = await _db.GetRentsAsync();
        var rent = rents.FirstOrDefault(r => r.UserId == user.Id && r.BoardGameId == gameId && r.Active);

        if (rent != null)
        {
            rent.Active = false;
            await _db.SaveRentAsync(rent);
        }

        await _gameService.GetGamesFromApiAsync();

        var games = await _db.GetGamesAsync();
        var game = games.FirstOrDefault(g => g.Id == gameId);

        return $"You returned {game.Name}";
    }
}