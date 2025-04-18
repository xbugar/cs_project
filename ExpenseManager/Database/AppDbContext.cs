using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Database;


public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Username=pv178;Password=postgres;Database=project");
}