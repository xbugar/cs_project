using ExpenseManager.Api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExpenseManager.Api.Requests;

public class AccountRequest(WebApplication app, ApiContext context) : Request(app, context)
{
    private record CreateRequest(string Name, string Description, decimal Balance, string Color);

    private async Task<IResult> CreateAccount(int userId, CreateRequest request)
    {
        EntityEntry<Account> account;
        try
        {
            account = await DbContext.Accounts
                .AddAsync(new Account
                {
                    Name = request.Name,
                    Description = request.Description,
                    Balance = request.Balance,
                    Color = request.Color,
                    UserId = userId
                });
            await DbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return Results.Conflict("Wrong account data");
        }

        return Results.Ok(account.Entity);
    }

    private async Task<IResult> UpdateAccount(int userId, int accountId, CreateRequest request)
    {
        var account = await DbContext.Accounts
            .FirstOrDefaultAsync(a => a.UserId == userId && a.Id == accountId);

        if (account == null)
            return Results.NotFound("Account not found");

        account.Name = request.Name;
        account.Description = request.Description;
        account.Balance = request.Balance;
        account.Color = request.Color;

        await DbContext.SaveChangesAsync();
        return Results.Ok(account);
    }

    private async Task<IResult> GetAccount(int userId, int accountId)
    {
        var account = await DbContext.Accounts
            .FirstOrDefaultAsync(a => a.UserId == userId && a.Id == accountId);

        if (account == null)
            return Results.NotFound("Account not found");

        return Results.Ok(account);
    }

    private async Task<IResult> DeleteAccount(int userId, int accountId)
    {
        var account = await DbContext.Accounts
            .FirstOrDefaultAsync(a => a.UserId == userId && a.Id == accountId);

        if (account == null)
            return Results.NotFound("Account not found");


        DbContext.Accounts.Remove(account);
        await DbContext.SaveChangesAsync();
        return Results.Ok();
    }

    public override void Map()
    {
        App.MapPost("/user/{userId}/account",
            async (int userId, [FromBody] CreateRequest request) =>
            await CreateAccount(userId, request)).WithName("CreateAccount");

        App.MapPut("/user/{userId}/account/{accountId}",
            async (int userId, int accountId, [FromBody] CreateRequest request) =>
            await UpdateAccount(userId, accountId, request)).WithName("UpdateAccount");

        App.MapGet("/user/{userId}/account/{accountId}",
            async (int userId, int accountId) =>
                await GetAccount(userId, accountId)).WithName("GetAccount");

        App.MapDelete("/user/{userId}/account/{accountId}", DeleteAccount);
    }
}