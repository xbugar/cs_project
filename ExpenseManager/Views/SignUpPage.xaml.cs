using System.Windows;
using System.Windows.Controls;

namespace ExpenseManager.Views;

public partial class SignUpPage : Page
{
    private readonly MainWindow _window;
    public SignUpPage(MainWindow window)
    {
        InitializeComponent();
        _window = window;
    }
    
    private void SwitchToSignIn_Click(object sender, RoutedEventArgs e)
    { 
        _window.NavigateTo(_window.Pages[0]);
    }
}
