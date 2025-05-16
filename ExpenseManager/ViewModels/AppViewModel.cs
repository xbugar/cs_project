using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Database;
using ExpenseManager.Models.Main;
using ExpenseManager.Services;
using ExpenseManager.Views;
using ExpenseManager.Views.Sign;
using Account = ExpenseManager.Models.Main.Account;

namespace ExpenseManager.ViewModels;

public partial class AppViewModel(User user) : ObservableObject
{
    [ObservableProperty] private User _user = user;


    [ObservableProperty] private List<Account> _accounts =
    [
        new() { Balance = 10, Color = "Green", Description = "sumn", Id = 1, Name = "First" },
        new() { Balance = 186450, Color = "Blue", Description = "sumn", Id = 2, Name = "Main" },
        new() { Balance = 13214, Color = "Red", Description = "sumn", Id = 3, Name = "Cartel" }
    ];

    [RelayCommand]
    private void LogOut()
    {
        var login = new LoginWindow();
        login.Show();

        Application.Current.MainWindow?.Close();

        Application.Current.MainWindow = login;
    }

    [RelayCommand]
    private void AddAccount()
    {
        var createAccountWindow = new CreateAccountWindow(user);
        createAccountWindow.Show();
    }
}