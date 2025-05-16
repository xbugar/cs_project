using ExpenseManager.Database;

namespace ExpenseManager.Services;

public static class Graph
{
    public record GraphData(decimal[] X, int[] Y, string Color);

    private static Task<GraphData> AccountGraphData(AppDbContext db, Account account) => Task.Run(() =>
    {
        var transactions = db.Transactions
            .Where(trans => trans.AccountId == account.Id && trans.PostingDate >= DateTime.UtcNow.AddMonths(-1))
            .GroupBy(t => t.PostingDate.Date)
            .Select(group => new
            {
                Date = group.Key,
                TotalAmount = group.Sum(t => t.Amount)
            }).ToList();
        
        var auxBalance = account.Balance;
        var x = new decimal[transactions.Count];
        var y = new int[transactions.Count];

        for (int i = transactions.Count - 1; i >= 0; i--)
        {
            auxBalance += transactions[i].TotalAmount;
            x[i] = auxBalance;
            y[i] = (int)(transactions[i].Date - DateTime.UtcNow.Date).TotalDays;
        }
        return new GraphData(x, y, account.Color);
    });

    public static async Task<GraphData[]> AccountsGraphData(AppDbContext db, List<Account> accounts)
    {
        var tasks = accounts.Select(account => AccountGraphData(db, account)).ToArray();
        var results = await Task.WhenAll(tasks);
        return results;
    }
}