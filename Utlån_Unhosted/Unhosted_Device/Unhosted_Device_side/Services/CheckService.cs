using Unhosted_Device_side.Data;
using Unhosted_Device_side.Models;

namespace Unhosted_Device_side.Services;

public class CheckService
{
    private readonly AppDatabase _db;
    private readonly CheckServiceAPI _checkServiceAPI;

    public CheckService(AppDatabase db, CheckServiceAPI checkServiceAPI)
    {
        _db = db;
        _checkServiceAPI = checkServiceAPI;
    }

    public async Task<List<ReturnedGames>> GetReturnedGamesAsync()
    {
        var borrows = await _checkServiceAPI.GetReturnedBorrowsAsync();
        var games = await _db.GetGamesAsync();

        if (borrows is null)
            return new List<ReturnedGames>();

        var returned = borrows
            .Where(b => !b.Active)
            .Select(b =>
            {
                var game = games.FirstOrDefault(g => g.Id == b.GameId);

                return new ReturnedGames
                {
                    BorrowId = b.Id,
                    GameId = b.GameId,
                    Email = b.Email,
                    Active = b.Active,
                    Name = game?.Name ?? "Unknown game",
                    Description = game?.Description ?? "",
                    ImagePath = game?.ImagePath
                };
            })
            .ToList();

        return returned;
    }

    public async Task<bool> GreenLightAsync(Guid borrowId)
    {
        return await _checkServiceAPI.GreenLightAsync(borrowId);
    }
}