using System.Windows;
using System.Windows.Controls;
using ExpenseManager.Ui.ViewModels;

namespace ExpenseManager.Ui.Views;

public partial class SignUpPage : Page
{
    private readonly LoginWindow _window;
    public SignUpPage(LoginWindow window)
    {
        InitializeComponent();
        _window = window;
    }
    
    private void SwitchToSignIn_Click(object sender, RoutedEventArgs e) =>
        _window.NavigateTo(_window.Pages[0]);
}
