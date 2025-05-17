using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSharpFunctionalExtensions;
using ExpenseManager.Database;
using ExpenseManager.Services;
using ExpenseManager.Views;
using ExpenseManager.Views.Sign;
using Microsoft.EntityFrameworkCore;
using ScottPlot;
using ScottPlot.WPF;

namespace ExpenseManager.ViewModels;

public partial class AppViewModel : ObservableObject
{
    [ObservableProperty] private User _user;

    private readonly WpfPlot? _plot;

    [ObservableProperty] private ObservableCollection<Account> _accounts = [];

    public AppViewModel(User user, WpfPlot plot)
    {
        User = user;
        _plot = plot;
        Initialize().ConfigureAwait(false);
    }

    private async Task Initialize()
    {
        await using var ctx = new AppDbContext();
        Accounts = new ObservableCollection<Account>(
            await ctx.Accounts
                .Where(a => a.UserId == User.Id)
                .OrderBy(a => a.Name)
                .ToListAsync()
        );

        if (_plot != null)
            await Graph.AccountsGraphData(Accounts.ToList(), _plot);
    }

    [RelayCommand]
    private static void LogOut()
    {
        var login = new LoginWindow();
        login.Show();

        Application.Current.MainWindow?.Close();

        Application.Current.MainWindow = login;
    }

    [RelayCommand]
    private void AddAccount()
    {
        var createAccountWindow = new CreateAccountWindow(User, Accounts);
        createAccountWindow.Show();
    }

    [RelayCommand]
    private void OpenAccount(Account account)
    {
        var detailsWindow = new AccountWindow(account, Accounts);
        detailsWindow.Show();
    }

    async partial void OnAccountsChanged(ObservableCollection<Account>? oldValue, ObservableCollection<Account> newValue)
    {
        try
        {
            if (_plot == null) return;
        
            // does not work
            _plot.Plot.Clear();
        
            _plot.Plot.Add.HorizontalLine(y: 0, color: Color.FromHex("#9D00FF"));

            await Graph.AccountsGraphData(newValue.ToList(), _plot);
            _plot.Refresh();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}