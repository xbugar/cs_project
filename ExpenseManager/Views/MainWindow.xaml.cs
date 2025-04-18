using System.Windows;
using System.Windows.Controls;

namespace ExpenseManager.Views;

public partial class MainWindow : Window
{
    public readonly Page[] Pages;
    
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new SignInPage(this));
        Pages = [new SignInPage(this), new SignUpPage(this)];
    }

    public void NavigateTo(Page page)
    {
        MainFrame.Navigate(page);
    }
}