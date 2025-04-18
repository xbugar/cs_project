using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Database;
using ExpenseManager.Models;
using ExpenseManager.Views;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.ViewModels;

public partial class SignViewModel : ObservableObject
{
    [ObservableProperty]
    private UserSign user = new UserSign();
    
    [RelayCommand]
    private void SignIn(PasswordBox passwordBox)
    {
        string password = passwordBox.Password;
        using UserContext ctx = new UserContext();
        
        User? dbUser = ctx.Users.FirstOrDefaultAsync(u => u.Email == User.Email).Result;
        if (dbUser == null || !PasswordHasher.VerifyPassword(password, dbUser.Salt, dbUser.HashedPassword))
        {
            MessageBox.Show("Either Email or Password is incorrect.");
            return;
        }

        MessageBox.Show($"Welcome, {dbUser.FirstName} {dbUser.LastName}!");
    }

    [RelayCommand]
    private void SignUp(PasswordBox passwordBox)
    {
        string password = passwordBox.Password;
        using UserContext ctx = new UserContext();
        
        if (ctx.Users.AnyAsync(u => u.Email == User.Email).Result)
        {
            MessageBox.Show("This Email is already registered.");
            return;
        }

        (byte[] hashedPassword, byte[] salt) = PasswordHasher.HashPassword(password);
        
        ctx.Users.AddAsync(new User
        {
            Email = User.Email,
            FirstName = User.FirstName,
            LastName = User.LastName,
            HashedPassword = hashedPassword,
            Salt = salt
        });
        ctx.SaveChangesAsync();
        
        MessageBox.Show("User registered successfully.");
    }
}