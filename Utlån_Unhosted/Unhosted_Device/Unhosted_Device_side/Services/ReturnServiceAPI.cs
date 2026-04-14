using System.Net.Http.Json;

namespace Unhosted_Device_side.Services;

public class ReturnServiceAPI
{
    private readonly HttpClient _httpClient;

    public ReturnServiceAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(bool Success, string Message)> ReturnGameAsync(Guid userId, Guid gameId)
    {
        var dto = new
        {
            UserId = userId,
            GameId = gameId
        };

        var response = await _httpClient.PostAsJsonAsync("api/ReturnGames", dto);
        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return (false, body);

        return (true, body);
    }
}