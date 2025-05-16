using System.Text.Json;
using CSharpFunctionalExtensions;
using ExpenseManager.Database;
using Account = ExpenseManager.Models.Main.Account;

namespace ExpenseManager.Services;

public static class UserService
{
    public record UserMainWindow(User User, Graph.GraphData GraphData);
}