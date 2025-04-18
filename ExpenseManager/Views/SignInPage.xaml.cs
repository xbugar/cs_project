using System.Windows;
using System.Windows.Controls;

namespace ExpenseManager.Views;

public partial class SignInPage : Page
{
    private readonly MainWindow _window;
    public SignInPage(MainWindow window)
    {
        _window = window;
        InitializeComponent();
    }

    private void SwitchToSignUp_Click(object sender, RoutedEventArgs e)
    {
        _window.NavigateTo(_window.Pages[1]);
    }
}
