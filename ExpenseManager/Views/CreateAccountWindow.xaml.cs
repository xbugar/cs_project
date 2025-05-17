using System.Collections.ObjectModel;
using System.Windows;
using ExpenseManager.Database;
using ExpenseManager.ViewModels;

namespace ExpenseManager.Views;

public partial class CreateAccountWindow : Window
{
    public CreateAccountWindow(User user, ObservableCollection<Account> accounts)
    {
        InitializeComponent();
        DataContext = new CreateAccountViewModel(user, accounts);
    }
}