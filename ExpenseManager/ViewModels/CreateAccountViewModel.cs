using CommunityToolkit.Mvvm.ComponentModel;
using ExpenseManager.Database;
using Account = ExpenseManager.Models.Main.Account;

namespace ExpenseManager.ViewModels;

public partial class CreateAccountViewModel(User user) : ObservableObject
{
    // [ObservableProperty] private Account _account = new ();
}