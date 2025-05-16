using System.Windows;
using System.Windows.Controls;

namespace ExpenseManager.Views.Sign;

public partial class LoginWindow : Window
{
    public readonly Page[] Pages;
    
    public LoginWindow()
    {
        InitializeComponent();
        Pages = [new SignInPage(this), new SignUpPage(this)];
        MainFrame.Navigate(Pages[0]);
    }

    public void NavigateTo(Page page)
    {
        MainFrame.Navigate(page);
    }
}