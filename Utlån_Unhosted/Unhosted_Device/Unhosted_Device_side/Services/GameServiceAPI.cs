using System.Net.Http.Json;
using Unhosted_Device_side.Models;

namespace Unhosted_Device_side.Services;

public class GameServiceAPI
{
    private readonly HttpClient _httpClient;

    public GameServiceAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Game>?> GetGamesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Game>>("api/RetrieveGames");
    }
}