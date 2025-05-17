using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Database;
using ExpenseManager.Services;
using Microsoft.EntityFrameworkCore;
using ScottPlot.WPF;

namespace ExpenseManager.ViewModels;

public partial class AccountViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Transaction> _transactions =
    [
        new()
        {
            Amount = 100,
            PostingDate = DateTime.Now,
            Description = "Test Transaction",
            Type = TransactionType.Entertainment,
            AccountId = 1
        },
        new()
        {
            Amount = 1000,
            PostingDate = DateTime.Now,
            Description = "Test Transaction",
            AccountId = 1
        },
        new()
        {
            Amount = 451.5465m,
            PostingDate = DateTime.Now,
            Description = "Test Transaction",
            AccountId = 1
        },
    ];

    [ObservableProperty] private Account _account;
    private readonly WpfPlot _plot;

    public AccountViewModel()
    {
        _account = new Account();
        _plot = new WpfPlot();
    }

    public AccountViewModel(Account account, WpfPlot plot)
    {
        _account = account;
        _plot = plot;
        Initialize().ConfigureAwait(false);
    }

    private async Task Initialize()
    {
        await using var ctx = new AppDbContext();
        var accounts = new List<Account> { Account };
        await Graph.AccountsGraphData(accounts, _plot);

        Transactions = new ObservableCollection<Transaction>(await ctx.Transactions
            .Where(t => t.AccountId == Account.Id)
            .ToListAsync());
    }

    [RelayCommand]
    private void DeleteAccount()
    {

    }

    [RelayCommand]
    private void AddTransaction()
    {
        
    }
}