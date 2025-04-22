using ExpenseManager.Api.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options =>
    options.UseNpgsql("Host=localhost;Username=postgres;Password=project;Database=pv178"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApiContext>();
    
    // var users = dbContext.Users.ToList();
    // string res = "";
    //
    // foreach (var user in users)
    // {
    //     res += $"User: {user.FirstName} {user.LastName}, Email: {user.Email}\n";
    // }
    
    app.MapGet("/", () => dbContext.Accounts.ToList());
    
    app.Run();
}


