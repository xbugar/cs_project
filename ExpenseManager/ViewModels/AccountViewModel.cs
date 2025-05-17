using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Database;
using ExpenseManager.Services;
using ExpenseManager.Views;
using Microsoft.EntityFrameworkCore;
using ScottPlot.WPF;

namespace ExpenseManager.ViewModels;

public partial class AccountViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Transaction> _transactions = [];

    [ObservableProperty] private ObservableCollection<Account> _accounts = [];

    [ObservableProperty] private DateTime? _from;

    [ObservableProperty] private DateTime? _to;

    [ObservableProperty] private Account _account;
    private readonly WpfPlot _plot;

    public AccountViewModel(Account account, WpfPlot plot, ObservableCollection<Account> accounts)
    {
        _account = account;
        _plot = plot;
        Initialize().ConfigureAwait(false);
        _accounts = accounts;
    }

    private async Task Initialize()
    {
        await using var ctx = new AppDbContext();
        var accounts = new List<Account> { Account };
        await Graph.AccountsGraphData(accounts, _plot);

        Transactions = new ObservableCollection<Transaction>(await ctx.Transactions
            .Where(t => t.AccountId == Account.Id)
            .OrderByDescending(t => t.PostingDate)
            .ToListAsync());
    }

    [RelayCommand]
    private async Task DeleteAccount(Window window)
    {
        await using var ctx = new AppDbContext();
        await ctx.Accounts
            .Where(a => a.Id == Account.Id)
            .ExecuteDeleteAsync();

        var accountToRemove = Accounts.FirstOrDefault(a => a.Id == Account.Id);
        if (accountToRemove != null)
            Accounts.Remove(accountToRemove);

        OnPropertyChanged(nameof(Accounts));
        window.Close();
    }

    [RelayCommand]
    private async Task DeleteTransaction(Transaction transaction)
    {
        await using var ctx = new AppDbContext();
        ctx.Transactions
            .Remove(transaction);

        Transactions.Remove(transaction);
        Account.Balance -= transaction.Amount;


        var acc = Accounts.FirstOrDefault(a => a.Id == Account.Id);
        if (acc != null)
        {
            var index = Accounts.IndexOf(acc);
            
            Accounts[index].Balance -= transaction.Amount;
            Accounts[index] = Accounts[index];
        }

        await ctx.Accounts.Where(a => a.Id == Account.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.Balance, Account.Balance));
        await ctx.SaveChangesAsync();

        OnPropertyChanged(nameof(Transactions));
        OnPropertyChanged(nameof(Account));
        OnPropertyChanged(nameof(Accounts));
    }

    [RelayCommand]
    private async Task SearchTransactions()
    {
        From = From?.ToUniversalTime();
        To = To?.ToUniversalTime();
        await using var ctx = new AppDbContext();
        Transactions = new ObservableCollection<Transaction>(await ctx.Transactions
            .Where(t => t.AccountId == Account.Id &&
                        (From == null || To == null || (t.PostingDate >= From && t.PostingDate <= To)))
            .OrderByDescending(t => t.PostingDate)
            .ToListAsync());

        OnPropertyChanged(nameof(Transactions));
    }


    [RelayCommand]
    private void AddTransaction()
    {
        var detailsWindow = new CreateTransactionWindow(Account, Transactions);
        detailsWindow.Show();
    }
}