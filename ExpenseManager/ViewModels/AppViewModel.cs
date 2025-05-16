using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Models.Main;
using ExpenseManager.Views;
using ExpenseManager.Views.Sign;

namespace ExpenseManager.ViewModels;

public partial class AppViewModel : ObservableObject
{
    [ObservableProperty] private UserMain _user = new UserMain();
    public AppViewModel(int userId)
    {
    // var response = UserService.GetUser(userId).Result;
    //
    // if (!response.IsSuccessStatusCode)
    // {
    //     MessageBox.Show("Error fetching user data");
    //     return;
    // }
    // var user = response.Content.ReadFromJsonAsync<UserMain>().Result;
    //
    // if (user == null)
    // {
    //     MessageBox.Show("Error fetching user data");
    //     return;
    // }
    //
    // _user = user;
    }
    

    [ObservableProperty] private List<Account> _accounts = [
        new () { Balance = 10, Color = "Green", Description = "sumn", Id = 1, Name = "First" },
        new () { Balance = 186450, Color = "Blue", Description = "sumn", Id = 2, Name = "Main" },
        new () { Balance = 13214, Color = "Red", Description = "sumn", Id = 3, Name = "Cartel" }
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
        var createAccountWindow = new CreateAccountWindow();
        createAccountWindow.Show();
    }
}
