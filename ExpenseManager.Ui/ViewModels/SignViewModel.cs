using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Ui.Models;
using ExpenseManager.Ui.Services;

namespace ExpenseManager.Ui.ViewModels;

public partial class SignViewModel : ObservableObject
{
    [ObservableProperty]
    private UserSign _user = new UserSign();
    
    private record UserSignResponse(int UserId);
    
    
    [RelayCommand]
    private async Task SignIn(PasswordBox passwordBox)
    {
        if (User.Email == null)
        {
            MessageBox.Show("Email is required");
            return;
        }
        
        var response = await SignService.SignIn(User, passwordBox.Password);
        if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            MessageBox.Show("Wrong credentials");
            return;
        }
        
        var responseContent = await response.Content.ReadFromJsonAsync<UserSignResponse>();
        
        if (responseContent == null)
        {
            MessageBox.Show("Failed to get user ID");
            return;
        }
        
        int userId = responseContent.UserId;
        
        var mainApp = new Views.AppWindow(userId);

        mainApp.Show();
        Application.Current.MainWindow?.Close();

        Application.Current.MainWindow = mainApp;
    }

    [RelayCommand]
    private async Task SignUp(PasswordBox passwordBox)
    {
        if (User.FirstName == null || User.LastName == null || User.Email == null)
        {
            MessageBox.Show("All fields are required");
            return;
        }
        
        var response = await SignService.SignUp(User, passwordBox.Password);
        if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            MessageBox.Show("User already exists");
            return;
        }
        
        var responseContent = await response.Content.ReadFromJsonAsync<UserSignResponse>();
        
        if (responseContent == null)
        {
            MessageBox.Show("Failed to get user ID");
            return;
        }
        
        int userId = responseContent.UserId;
        
        var mainApp = new Views.AppWindow(userId);
        mainApp.Show();
        Application.Current.MainWindow?.Close();
        
        Application.Current.MainWindow = mainApp;
    }
}
