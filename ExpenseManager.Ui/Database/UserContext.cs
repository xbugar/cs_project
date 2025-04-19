using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Ui.Database;


public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=project;Database=pv178");
}