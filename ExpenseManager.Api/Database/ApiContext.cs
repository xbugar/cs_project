using Microsoft.EntityFrameworkCore;
using ExpenseManager.Api.Database.MockData;

namespace ExpenseManager.Api.Database;

public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSeeding((ctx, _) =>
            {
                try
                {
                    Seeding.Seed(ctx).Wait();
                }
                catch (Exception)
                {
                    Console.WriteLine("could not seed as it already exists");
                }
            });
}