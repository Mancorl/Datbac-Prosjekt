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

    public async Task<int> DeleteGameAsync(Guid id)
    {
        await InitAsync();
        return await _database!.DeleteAsync<GameEntity>(id);
    }

    public async Task<int> DeleteRentAsync(Guid id)
    {
        await InitAsync();
        return await _database!.DeleteAsync<RentEntity>(id);
    }

   
}