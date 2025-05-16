using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Database;
using ExpenseManager.Models;
using ExpenseManager.Services;

namespace ExpenseManager.ViewModels;

public partial class SignViewModel : ObservableObject
{
    [ObservableProperty]
    private UserSign _user = new UserSign();
    
    [RelayCommand]
    private async Task SignIn(PasswordBox passwordBox)
    {
        if (User.Email == null)
        {
            MessageBox.Show("Email is required");
            return;
        }
        
        var res = await AuthService.SignIn(User.Email, passwordBox.Password);
        if (res.IsFailure)
        {
            MessageBox.Show(res.Error);
            return;
        }

        var userId = res.Value.Id;
        
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
        
        var res = await AuthService.SignUp(User.FirstName, User.LastName, User.Email, passwordBox.Password);
        if (res.IsFailure)
        {
            MessageBox.Show(res.Error);
            return;
        }
        
        var userId = res.Value.Id;
        
        var mainApp = new Views.AppWindow(userId);
        mainApp.Show();
        Application.Current.MainWindow?.Close();
        
        Application.Current.MainWindow = mainApp;
    }
}
