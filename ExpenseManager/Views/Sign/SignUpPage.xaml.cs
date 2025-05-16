using System.Windows;
using System.Windows.Controls;

namespace ExpenseManager.Views.Sign;

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
