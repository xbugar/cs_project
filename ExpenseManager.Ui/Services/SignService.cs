using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Controls;
using ExpenseManager.Ui.Models;
using ExpenseManager.Ui.ViewModels;

namespace ExpenseManager.Ui.Services;

public static class SignService
{
    public static async Task<HttpResponseMessage> SignIn(UserSign user, string password)
    {
        var response = await ApiClient.Client
            .PostAsync("/auth/signin", new StringContent(
            
                JsonSerializer.Serialize(new { user.Email, Password = password }),
                Encoding.UTF8,
                "application/json"
            ));

        return response;
    }
    
    public static async Task<HttpResponseMessage> SignUp(UserSign user, string password)
    {
        var response = await ApiClient.Client
            .PostAsync("/auth/signup", new StringContent(
                JsonSerializer.Serialize(new
                {
                    user.FirstName, user.LastName, user.Email, Password = password
                }),
                Encoding.UTF8,
                "application/json"
            ));

        return response;
    }
}