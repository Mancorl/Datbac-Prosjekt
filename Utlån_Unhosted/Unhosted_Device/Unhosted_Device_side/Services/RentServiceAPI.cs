using System.Net.Http.Json;
using Unhosted_Device_side.Data.Tables;

namespace Unhosted_Device_side.Services;

public class RentServiceAPI
{
    private readonly HttpClient _httpClient;

    public RentServiceAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(bool Success, string Message)> BorrowGameAsync(Guid userId, Guid gameId)
    {
        
            var dto = new
            {
                UserId = userId,
                GameId = gameId
            };

            var response = await _httpClient.PostAsJsonAsync("api/BorrowGames", dto);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return (false, body);

            return (true, body);
    }
   public async Task<List<RentEntity>?> GetUserRentsAsync(Guid userId)
{
    return await _httpClient.GetFromJsonAsync<List<RentEntity>>(
        $"api/BorrowGames/user/{userId}");
}
}