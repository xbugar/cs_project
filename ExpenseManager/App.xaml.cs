using System.Windows;
using ExpenseManager.Database;
using ExpenseManager.Services;
using ExpenseManager.ViewModels;
using ExpenseManager.Views.Sign;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql("Host=localhost;Username=postgres;Password=project;Database=pv178"));
    }
}