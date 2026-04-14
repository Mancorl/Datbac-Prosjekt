using System.Net.Http.Json;
using Unhosted_Device_side.Data.Tables;

namespace Unhosted_Device_side.Services;

public class CheckServiceAPI
{
    private readonly HttpClient _httpClient;

    public CheckServiceAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RentEntity>?> GetReturnedBorrowsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<RentEntity>>(
            "api/AdminReturnCheckController");
    }

    public async Task<bool> GreenLightAsync(Guid borrowId)
    {
        var response = await _httpClient.DeleteAsync($"api/GameCheckerController/{borrowId}");
        return response.IsSuccessStatusCode;
    }
}