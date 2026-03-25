using SQLite;

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

        await _database.CreateTableAsync<UserClass>();
        //await _database.CreateTableAsync<LoanEntity>();

        _initialized = true;
    }

    public async Task<List<UserClass>> GetUsersAsync()
    {
        await InitAsync();
        return await _database!.Table<UserClass>().ToListAsync();
    }

    public async Task<int> SaveUserAsync(UserClass user)
    {
        await InitAsync();

        if (user.Id != 0)
            return await _database!.UpdateAsync(user);

        return await _database!.InsertAsync(user);
    }
}