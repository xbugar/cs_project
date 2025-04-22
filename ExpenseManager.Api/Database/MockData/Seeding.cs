using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Api.Database.MockData;

public static class Seeding
{
    private static void SeedAccount(DbContext ctx, User user, Icon icon, decimal amount, int order)
    {
        var promise = ctx.Set<Account>().AddAsync(new Account
        {
            UserId = user.Id,
            Name = $"{user.LastName}'s {order}-th Account",
            Color = "#FF0000",
            IconId = icon.Id,
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

    private static void SeedUser(DbContext ctx, string firstName, string lastName, string email, Icon icon)
    {
        User user = ctx.Set<User>().Add(new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = [],
        }).Entity;
        
        ctx.SaveChanges();

        SeedAccount(ctx, user, icon, 69m, 1);
        SeedAccount(ctx, user, icon, 128m, 2);
        SeedAccount(ctx, user, icon, 9999999999.48m, 3);
    }
    
    public static async Task<bool> Seed(DbContext ctx)
    {
        try
        {
            var entity = ctx.Set<Icon>().Add(new Icon
            {
                Name = "Default",
                Data = File.ReadAllBytes("Database/MockData/TestIcon.svg")
            });

            await ctx.SaveChangesAsync();
            
            SeedUser(ctx, "John", "Doe", "john.doe@email.com", entity.Entity);
            SeedUser(ctx, "Andrej", "Bugar", "bugar@email.com", entity.Entity);
            SeedUser(ctx, "Barack", "Obama", "barack.obama@email.com", entity.Entity);
            SeedUser(ctx, "Joe", "Biden", "iamsotired@email.com", entity.Entity);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong while seeding the database");
            Console.WriteLine("No seeding was performed");
            throw;
        }
    }
}