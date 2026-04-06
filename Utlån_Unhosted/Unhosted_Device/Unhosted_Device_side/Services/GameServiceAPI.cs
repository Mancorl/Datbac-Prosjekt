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

    public async Task<bool> CreateGameAsync(Game game, Stream? imageStream)
{
    var content = new MultipartFormDataContent();

    content.Add(new StringContent(game.Name ?? ""), "Name");
    content.Add(new StringContent(game.Quantity.ToString()), "Quantity");
    content.Add(new StringContent(game.Description ?? ""), "Description");
    content.Add(new StringContent(game.Loanable.ToString()), "Loanable");
    content.Add(new StringContent(game.ImagePath ?? "images/Default.jpg"), "ImagePath");

    if (imageStream != null)
    {
        var fileContent = new StreamContent(imageStream);
        fileContent.Headers.ContentType =
            new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

        content.Add(fileContent, "Image", "upload.jpg");
    }

    var response = await _httpClient.PostAsync("api/AddGames", content);

    return response.IsSuccessStatusCode;
}


    public async Task<bool> UpdateGameAsync(Game game)
{
    var content = new MultipartFormDataContent();

    content.Add(new StringContent(game.Id.ToString()), "Id");
    content.Add(new StringContent(game.Name ?? ""), "Name");
    content.Add(new StringContent(game.Quantity.ToString()), "Quantity");
    content.Add(new StringContent(game.Description ?? ""), "Description");
    content.Add(new StringContent(game.Loanable.ToString()), "Loanable");
    content.Add(new StringContent(game.ImagePath ?? "images/Default.jpg"), "ImagePath");

    var response = await _httpClient.PostAsync("api/EditGames", content);
    return response.IsSuccessStatusCode;
}

public async Task<bool> DeleteGameAsync(Guid id)
{
    var response = await _httpClient.DeleteAsync($"api/DeleteGame/{id}");
    return response.IsSuccessStatusCode;
}

}