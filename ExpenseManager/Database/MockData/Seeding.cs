using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Database.MockData;

public static class Seeding
{
    private static readonly PasswordHasher<User> Auth = new();
    private static void SeedAccount(DbContext ctx, User user, decimal amount, int order, string color)
    {
        var promise = ctx.Set<Account>().AddAsync(new Account
        {
            UserId = user.Id,
            Name = $"{user.LastName}'s {order}-th Account",
            Color = color,
            Balance = amount
        });

        var account = promise.Result.Entity;
        
        ctx.SaveChanges();
        for (int i = 0; i < 10; i++)
        {
            ctx.Set<Transaction>().AddAsync(new Transaction
            {
                Amount = 7.00m,
                Description = "Test Expense",
                AccountId = account.Id,
                Type = Transaction.TransactionType.Entertainment
            });
        }
    }

    private static void SeedUser(DbContext ctx, string firstName, string lastName, string email)
    {
        User user = ctx.Set<User>().Add(new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = Auth.HashPassword(new User(), "pes"),
        }).Entity;
        
        ctx.SaveChanges();

        SeedAccount(ctx, user, 69m, 1, "#FF0000");
        SeedAccount(ctx, user, 128m, 2, "#00FF00");
        SeedAccount(ctx, user, 9999999999.48m, 3, "#0000FF");
    }
    
    public static async Task<bool> Seed(DbContext ctx)
    {
        try
        {
            await ctx.SaveChangesAsync();
            
            SeedUser(ctx, "John", "Doe", "john.doe@email.com");
            SeedUser(ctx, "Andrej", "Bugar", "burger");
            SeedUser(ctx, "Barack", "Obama", "barack.obama@email.com");
            SeedUser(ctx, "Joe", "Biden", "iamsotired@email.com");
            
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            Console.WriteLine("Something went wrong while seeding the database");
            Console.WriteLine("No seeding was performed");
            throw;
        }
    }
}