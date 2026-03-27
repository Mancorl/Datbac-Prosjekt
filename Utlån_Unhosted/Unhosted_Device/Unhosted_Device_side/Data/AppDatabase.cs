using SQLite;
using Unhosted_Device_side.Data.Tables;
using Unhosted_Device_side.Services;
using Unhosted_Device_side.Common;

namespace Unhosted_Device_side.Data;

public class AppDatabase
{
    private SQLiteAsyncConnection? _database;
    private bool _initialized;

    public async Task InitAsync()
    {
        if (_initialized && _database is not null)
            return;

        _database = new SQLiteAsyncConnection(
            Constants.DatabasePath,
            Constants.Flags);

        await _database.CreateTableAsync<UserEntity>();
        await _database.CreateTableAsync<GameEntity>();
        await _database.CreateTableAsync<RentEntity>();

        var existingGames = await _database.Table<GameEntity>().ToListAsync();

    if (existingGames.Count == 0)
    {
        var games = new List<GameEntity>
        {
            new GameEntity
            {
                Id = Guid.NewGuid(),
                Name = "Catan",
                Quantity = 3,
                TotalQuantity = 3,
                Loanable = true,
                Description = "Trade, build, and settle.",
                ImagePath = "images/catan.jpg"
            },
            new GameEntity
            {
                Id = Guid.NewGuid(),
                Name = "Monopoly",
                Quantity = 2,
                TotalQuantity = 2,
                Loanable = true,
                Description = "Classic property trading game.",
                ImagePath = "images/monopoly.jpg"
            },
            new GameEntity
            {
                Id = Guid.NewGuid(),
                Name = "Chess",
                Quantity = 9,
                TotalQuantity = 9,
                Loanable = true,
                Description = "Strategic board game for two players.",
                ImagePath = "images/chess.jpg"
            }
        };

        await _database.InsertAllAsync(games);
    }


        _initialized = true;
    }

    public async Task<List<UserEntity>> GetUsersAsync()
    {
        await InitAsync();
        return await _database!.Table<UserEntity>().ToListAsync();
    }

    public async Task<int> SaveUserAsync(UserEntity user)
    {
        await InitAsync();

        return await _database!.InsertOrReplaceAsync(user);
    }

    public async Task<List<GameEntity>> GetGamesAsync()
    {
        await InitAsync();
        return await _database!.Table<GameEntity>()
            .OrderBy(g => g.Name)
            .ToListAsync();
    }
    public async Task<int> SaveGameAsync(GameEntity game)
    {
        await InitAsync();
        return await _database!.InsertOrReplaceAsync(game);
    }

     public async Task<List<RentEntity>> GetRentsAsync()
    {
        await InitAsync();
        return await _database!.Table<RentEntity>().ToListAsync();
    }

    public async Task<int> SaveRentAsync(RentEntity rent)
    {
        await InitAsync();
        return await _database!.InsertOrReplaceAsync(rent);
    }

   
}