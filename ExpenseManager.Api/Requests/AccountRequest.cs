using ExpenseManager.Api.Database;

namespace ExpenseManager.Api.Requests;

public class AccountRequest(WebApplication app, ApiContext context) : Request(app, context)
{
    
    
    
    public override void Map()
    {
        throw new NotImplementedException();
    }
}