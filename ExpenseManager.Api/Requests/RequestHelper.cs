using ExpenseManager.Api.Database;

namespace ExpenseManager.Api.Requests;

public static class RequestHelper
{
    public static void MapRequests(WebApplication app, ApiContext context)
    {
        var  auth = new AuthRequest(app, context);
        auth.Map();
    }
}