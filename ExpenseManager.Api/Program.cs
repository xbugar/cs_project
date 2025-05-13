using ExpenseManager.Api.Database;
using ExpenseManager.Api.Requests;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(3000));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();


using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApiContext>();

RequestHelper.MapRequests(app, dbContext);

app.MapGet("/", () => dbContext.Users.ToList());
app.MapGet("/accounts", () =>
    dbContext.Accounts.Select(a => new
    {
        a.Id,
        a.Name,
        a.Balance,
    }).ToList());

app.Run();
