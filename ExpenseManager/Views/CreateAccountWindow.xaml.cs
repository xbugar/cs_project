using System.Windows;
using ExpenseManager.Database;
using ExpenseManager.ViewModels;

namespace ExpenseManager.Views;

public partial class CreateAccountWindow : Window
{
    public CreateAccountWindow(User user)
    {
        InitializeComponent();
        DataContext = new CreateAccountViewModel(user);
    }
}