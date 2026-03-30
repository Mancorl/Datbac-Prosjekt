using System.Net.Http.Json;
using System.Text.Json;
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
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return await _httpClient.GetFromJsonAsync<List<Game>>("api/RetrieveGames", options);
    }

    public async Task<bool> CreateGameAsync(Game game)
    {
        var response = await _httpClient.PostAsJsonAsync("api/CreateGame", game);
        return response.IsSuccessStatusCode;
    }
}