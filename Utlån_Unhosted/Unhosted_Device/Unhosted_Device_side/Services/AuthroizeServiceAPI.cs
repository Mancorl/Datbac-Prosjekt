using System.Net.Http.Json;
using Unhosted_Device_side.Models;

namespace Unhosted_Device_side.Services;

public class AuthorizeServiceAPI
{
    private readonly HttpClient _httpClient;

    public AuthorizeServiceAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool?> GetIsAuthorizedAsync(Guid userId)
    {
        
            var response = await _httpClient.GetAsync($"api/iSAuthorized/{userId}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<bool>();
       
    }

  

    public async Task<List<UnauthorUsers>?> GetUnauthorizedUsersAsync()
    {
        
        return await _httpClient.GetFromJsonAsync<List<UnauthorUsers>>("api/GetUnauthorizedController");

    }

    public async Task<bool> AuthorizeUserAsync(Guid userId)
    {
        var response = await _httpClient.PostAsync($"api/RegisterUser/{userId}?reg=true", null);
        return response.IsSuccessStatusCode;
    }
}