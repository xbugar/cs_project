using System.Net.Http;

namespace ExpenseManager.Ui.Services;

public static class ApiClient
{
    public static readonly HttpClient Client = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:3000/")
    };
}