using ExpenseManager.Api.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Api.Requests;

public class AuthRequest(WebApplication app, ApiContext context) : Request(app, context)
{
    private static readonly PasswordHasher<User> Auth = new();

    private record LoginRequest(string Email, string Password);
    private record RegisterRequest(string FirstName, string LastName, string Email, string Password);


    private async Task<IResult> MapSignIn(LoginRequest request)
    {
        var user = await DbContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);


        if (user == null
            || Auth.VerifyHashedPassword(user, user.PasswordHash, request.Password) ==
            PasswordVerificationResult.Failed)
        {
            return Results.Conflict("Wrong credentials");
        }

        Console.WriteLine($"Logged In -> email: {user.Email}\n password: {request.Password}");
        return Results.Ok(user.Id);
    }

    private async Task<IResult> MapSignUp(RegisterRequest request)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = Auth.HashPassword(new User(), request.Password),
        };

        try
        {
            await DbContext.Users.AddAsync(user);
            await DbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Conflict("User already exists");
        }

        Console.WriteLine(
            $"name: {user.FirstName} {user.LastName}\n email: {user.Email}\n password: {request.Password}");
        return Results.Ok( new {
            user.Id,
            user.FirstName
        });
    }

    public override void Map()
    {
        App.MapPost("/auth/signin", MapSignIn);
        App.MapPost("/auth/signup", MapSignUp);
    }
}