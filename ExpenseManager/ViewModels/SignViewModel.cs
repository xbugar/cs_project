using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSharpFunctionalExtensions;
using ExpenseManager.Database;
using ExpenseManager.Models;
using ExpenseManager.Services;

namespace ExpenseManager.ViewModels;

public partial class SignViewModel : ObservableObject
{
    [ObservableProperty]
    private UserSign _user = new UserSign();
    
    [RelayCommand]
    private async Task SignIn(string password)
    {
        if (User.Email == null)
        {
            MessageBox.Show("Email is required");
            return;
        }
        
        var (_, isFailure, user, error) = await AuthService.SignIn(User.Email, password);
        if (isFailure)
        {
            MessageBox.Show(error);
            return;
        }

        var mainApp = new Views.AppWindow(user);
        mainApp.Show();
        
        Application.Current.MainWindow?.Close();
        Application.Current.MainWindow = mainApp;
    }

    [RelayCommand]
    private async Task SignUp(string password)
    {
        if (User.FirstName == null || User.LastName == null || User.Email == null)
        {
            MessageBox.Show("All fields are required");
            return;
        }
        
        var res = await AuthService.SignUp(User.FirstName, User.LastName, User.Email, password);
        if (res.IsFailure)
        {
            MessageBox.Show(res.Error);
            return;
        }
        
        var user = res.Value;
        
        var mainApp = new Views.AppWindow(user);
        mainApp.Show();
        Application.Current.MainWindow?.Close();
        
        Application.Current.MainWindow = mainApp;
    }
}
