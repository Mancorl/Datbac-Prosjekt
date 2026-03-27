using Unhosted_Device_side.Data;
using Unhosted_Device_side.Data.Tables;
using Unhosted_Device_side.Services;

namespace Unhosted_Device_side.Services;

public class GameService
{
    private readonly AppDatabase _database;
    private readonly GameServiceAPI _gameServiceAPI;

    public GameService(AppDatabase database, GameServiceAPI gameServiceAPI)
    {
        _database = database;
        _gameServiceAPI = gameServiceAPI;
    }

    public async Task<List<GameEntity>> GetLocalGamesAsync()
    {
        return await _database.GetGamesAsync();
    }

    public async Task GetGamesFromApiAsync()
    {
        var apiGames = await _gameServiceAPI.GetGamesAsync();
        if (apiGames is null)
            return;

        foreach (var game in apiGames)
        {
            var entity = new GameEntity
            {
                Id = game.Id,
                Name = game.Name,
                Quantity = game.Quantity,
                TotalQuantity = game.TotalQuantity,
                Loanable = game.Loanable,
                ImagePath = game.ImagePath,
                Description = game.Description
            };

            await _database.SaveGameAsync(entity);
        }
    }
}