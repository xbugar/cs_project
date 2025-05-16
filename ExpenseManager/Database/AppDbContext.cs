using ExpenseManager.Database.MockData;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public AppDbContext() { }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=project;Database=pv178");
        optionsBuilder
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
}