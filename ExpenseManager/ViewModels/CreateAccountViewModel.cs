using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Database;

namespace ExpenseManager.ViewModels;

public partial class CreateAccountViewModel(User user, ObservableCollection<Account> accounts) : ObservableObject
{
    [ObservableProperty] private Account _account = new ();

    [RelayCommand]
    private async Task CreateAccount(Window window)
    {
        await using var ctx = new AppDbContext();
        Account.UserId = user.Id;

        Account account;
        try
        {
            account = (await ctx.Accounts.AddAsync(Account)).Entity;
            await ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            MessageBox.Show("All fields are required");
            return;
        }
        
        accounts.Add(account);
        OnPropertyChanged(nameof(accounts));
        window.Close();
    }
}