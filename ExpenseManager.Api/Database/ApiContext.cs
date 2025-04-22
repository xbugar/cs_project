using Microsoft.EntityFrameworkCore;
using ExpenseManager.Api.Database.MockData;

namespace ExpenseManager.Api.Database;

public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Expense> Transactions { get; set; }
    public DbSet<Icon> Icons { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql("Host=localhost;Username=postgres;Password=project;Database=pv178;Include Error Detail=true")
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Error)
            .UseSeeding((ctx, _) =>
            {
                // Seeding.Seed(ctx).Wait();
            });
}