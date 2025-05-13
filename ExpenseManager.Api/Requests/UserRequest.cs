using ExpenseManager.Api.Database;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Api.Requests;

public class UserRequest(WebApplication app, ApiContext context) : Request(app, context)
{
    private record GetRequest(int Id);

    private async Task<IResult> MapGet(GetRequest request)
    {
        var user = await DbContext.Users
            .Include(u => u.Accounts)
            .ThenInclude(a => a.Transactions.Where(t => t.PostingDate >= DateTime.UtcNow.AddMonths(-1)))
            .FirstOrDefaultAsync(u => u.Id == request.Id);


        if (user == null)
        {
            return Results.NotFound("User not found");
        }


        return Results.Ok(user);
    }


    public override void Map()
    {
        throw new NotImplementedException();
    }
}