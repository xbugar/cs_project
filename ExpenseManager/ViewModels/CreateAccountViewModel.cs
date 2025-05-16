using CommunityToolkit.Mvvm.ComponentModel;
using ExpenseManager.Models.Main;

namespace ExpenseManager.ViewModels;

public partial class CreateAccountViewModel : ObservableObject
{
    [ObservableProperty] private Account account = new ();
}