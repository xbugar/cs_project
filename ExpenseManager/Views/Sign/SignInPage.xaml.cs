using System.Windows;
using System.Windows.Controls;

namespace ExpenseManager.Views.Sign;

public partial class SignInPage : Page
{
    private readonly LoginWindow _window;
    public SignInPage(LoginWindow window)
    {
        _window = window;
        InitializeComponent();
    }

    private void SwitchToSignUp_Click(object sender, RoutedEventArgs e) =>
        _window.NavigateTo(_window.Pages[1]);
}
