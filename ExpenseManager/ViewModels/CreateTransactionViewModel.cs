using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Database;
using ExpenseManager.Services;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.ViewModels;

public partial class CreateTransactionViewModel(Account account, ObservableCollection<Transaction> transactions)
    : ObservableObject
{
    [ObservableProperty] private Transaction _transaction = new();
    [ObservableProperty] private ObservableCollection<Transaction> _transactions = transactions;
    [ObservableProperty] private Account _account = account;


    [RelayCommand]
    private async Task CreateTransaction(Window window)
    {
        await using var ctx = new AppDbContext();
        Transaction.AccountId = Account.Id;
        Transaction.PostingDate = Transaction.PostingDate.ToUniversalTime();

        Transaction transaction;
        try
        {
            transaction = (await ctx.Transactions.AddAsync(Transaction)).Entity;
            await ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageBox.Show("All fields are required");
            return;
        }
        
        Transactions.Add(transaction);
        Account.Balance += transaction.Amount;
        
        await ctx.Accounts.Where(a => a.Id == Account.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.Balance, Account.Balance));
        
        OnPropertyChanged(nameof(transactions));
        OnPropertyChanged(nameof(account));
        window.Close();
    }
    
    public async Task ProcessDroppedFile(string[] files)
    {
        var result = await Files.ProcessDroppedFile(files, Account);
        
        foreach (var res in result)
            Account.Balance += res.Amount;
        
        Transactions.Clear();
        await using var ctx = new AppDbContext();
        
        await ctx.Accounts.Where(a => a.Id == Account.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.Balance, Account.Balance));
        
        ctx.Transactions.Where(t => t.AccountId == Account.Id)
            .ToList()
            .ForEach(Transactions.Add);
        
        OnPropertyChanged(nameof(transactions));
        OnPropertyChanged(nameof(account));
        
    }
}