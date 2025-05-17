
using CSharpFunctionalExtensions;
using ExpenseManager.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Services;

public static class AuthService
{
    private static readonly PasswordHasher<User> Auth = new();

    public static async Task<Result<User>> SignIn(string email, string password)
    {
        await using var ctx = new AppDbContext();
        var user = await ctx.Users
            .FirstOrDefaultAsync(u => u.Email == email);


        if (user == null
            || Auth.VerifyHashedPassword(user, user.PasswordHash, password) ==
            PasswordVerificationResult.Failed)
        {
            return Result.Failure<User>("Wrong credentials");
        }

        return user;
    }

    public static async Task<Result<User>> SignUp(string firstName, string lastName, string email, string password)
    {
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = Auth.HashPassword(new User(), password)
        };

        try
        {
            await using var ctx = new AppDbContext();
            await ctx.Users.AddAsync(user);
            await ctx.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure<User>("User already exists");
        }

        return Result.Success(user);
    }
}