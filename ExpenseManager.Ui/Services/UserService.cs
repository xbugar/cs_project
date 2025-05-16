using System.Net.Http;
using System.Text;
using System.Text.Json;
using ExpenseManager.Ui.Models.Main;

namespace ExpenseManager.Ui.Services;

public static class UserService
{
    public static async Task<(UserMain? user, object? graphData)> GetUser(int userId)
    {
        var response = await ApiClient.Client
            .GetAsync($"/user/{userId}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<UserResponseModel>(content, options);
            return (result?.user, result?.graphData);
        }
    
        // Handle error cases appropriately
        return (null, null);
    }
}
