using ExpenseManager.Api.Database;
using ExpenseManager.Api.Logic;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Api.Requests;

public class UserRequest(WebApplication app, ApiContext context) : Request(app, context)
{
    private async Task<IResult> GetUser(int userId)
    {
        Console.WriteLine("pica");
        var user = await DbContext.Users
            .Include(u => u.Accounts)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return Results.NotFound("Unauthorized");
        
        var graphData = await Graph.AccountsGraphData(DbContext, user.Accounts);
        
        return Results.Ok(new
        {
            user = user,
            graphData = graphData
        });
    }


    public override void Map()
    {
        App.MapGet("/user/{userId:int}", async (int userId) => await GetUser(userId));
    }
}