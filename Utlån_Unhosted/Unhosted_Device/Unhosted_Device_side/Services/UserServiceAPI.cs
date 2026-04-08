using System.Net.Http.Json;
using Unhosted_Device_side.Data.Tables;
using Unhosted_Device_side.Models;

namespace Unhosted_Device_side.Services;

public class UserServiceAPI
{
    private readonly HttpClient _httpClient;

    public UserServiceAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(bool Success, string Message, AdminUserResult? User)> SendUserForAuthorizationAsync(UserEntity user)
    {
        
            var dto = new
            {

                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                First = user.First,
                Last = user.Last
            };

            var response = await _httpClient.PostAsJsonAsync("api/User", dto);

            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return (false, body, null);

            var result = await response.Content.ReadFromJsonAsync<AdminUserResult>();
            return (true, "User sent successfully", result);
       
    }
}