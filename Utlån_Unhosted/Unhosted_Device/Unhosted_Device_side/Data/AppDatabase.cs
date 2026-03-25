using SQLite;
using Unhosted_Device_side.Data.Tables;

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
}