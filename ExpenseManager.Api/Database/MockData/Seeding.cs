using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Api.Database.MockData;

public static class Seeding
{
    private static void SeedAccount(DbContext ctx, User user, decimal amount, int order)
    {
        var promise = ctx.Set<Account>().AddAsync(new Account
        {
            UserId = user.Id,
            Name = $"{user.LastName}'s {order}-th Account",
            Color = "#FF0000",
            Balance = amount,
        });

        var account = promise.Result.Entity;
        
        ctx.SaveChanges();
        for (int i = 0; i < 10; i++)
        {
            ctx.Set<Transaction>().AddAsync(new Expense
            {
                Amount = 7.00m,
                Description = "Test Expense",
                AccountId = account.Id,
            });

            ctx.Set<Transaction>().AddAsync(new Income
            {
                Amount = 9.00m,
                Description = "Test Income",
                AccountId = promise.Result.Entity.Id,
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
            PasswordHash = "",
        }).Entity;
        
        ctx.SaveChanges();

        SeedAccount(ctx, user, 69m, 1);
        SeedAccount(ctx, user, 128m, 2);
        SeedAccount(ctx, user, 9999999999.48m, 3);
    }
    
    public static async Task<bool> Seed(DbContext ctx)
    {
        try
        {
            await ctx.SaveChangesAsync();
            
            SeedUser(ctx, "John", "Doe", "john.doe@email.com");
            SeedUser(ctx, "Andrej", "Bugar", "bugar@email.com");
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