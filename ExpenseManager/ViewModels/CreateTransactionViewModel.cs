using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Database;
using ExpenseManager.Services;

namespace ExpenseManager.ViewModels;

public partial class CreateTransactionViewModel(Account account, ObservableCollection<Transaction> transactions) : ObservableObject
{
    [ObservableProperty] private Transaction _transaction = new ();


    [RelayCommand]
    private async Task CreateTransaction(Window window)
    {
        await using var ctx = new AppDbContext();
        Transaction.AccountId = account.Id;

        Transaction transaction;
        try
        {
            transaction = (await ctx.Transactions.AddAsync(Transaction)).Entity;
            await ctx.SaveChangesAsync();
        }
        catch (Exception)
        {
            MessageBox.Show("All fields are required");
            return;
        }
        
        transactions.Add(transaction);
        OnPropertyChanged(nameof(transactions));
        window.Close();
    }
    
    public async Task ProcessDroppedFile(string[] files)
    {
        var result = await Files.ProcessDroppedFile(files, account);
        
        transactions.Clear();
        await using var ctx = new AppDbContext();
        
        ctx.Transactions.Where(t => t.AccountId == account.Id)
            .ToList()
            .ForEach(transactions.Add);
    }
}