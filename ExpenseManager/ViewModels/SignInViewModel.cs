using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Database;
using ExpenseManager.Models;
using ExpenseManager.Views;

namespace ExpenseManager.ViewModels;

public partial class SignInViewModel : ObservableObject
{
    [ObservableProperty]
    private UserSign user = new UserSign();
    
    [RelayCommand]
    private void SignIn()
    {
        await using UserContext ctx = new UserContext();
        
        

        // Here you would typically check the credentials against a database or an API.
        // For this example, we'll just show a message box.
        MessageBox.Show($"Welcome, {User.FirstName} {User.LastName}!");
    }
}