using CommunityToolkit.Mvvm.ComponentModel;
using ExpenseManager.Ui.Models.Main;

namespace ExpenseManager.Ui.ViewModels;

public partial class CreateAccountViewModel : ObservableObject
{
    [ObservableProperty] private Account account = new ();
}