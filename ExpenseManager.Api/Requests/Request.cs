using ExpenseManager.Api.Database;

namespace ExpenseManager.Api.Requests;

public abstract class Request(WebApplication app, ApiContext dbContext)
{
    protected readonly WebApplication App = app;
    protected readonly ApiContext DbContext = dbContext;

    public abstract void Map();
}