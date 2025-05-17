using ExpenseManager.Database;
using ExpenseManager.Models;
using ScottPlot;
using ScottPlot.WPF;

namespace ExpenseManager.Services;

public static class Graph
{
    private record GraphData(int[] X, decimal[] Y, string Color);

    private static Task<GraphData> AccountGraphData(Account account) => Task.Run(() =>
    {
        using var db = new AppDbContext();
        var transactions = db.Transactions
            .Where(trans => trans.AccountId == account.Id && trans.PostingDate >= DateTime.UtcNow.AddMonths(-1))
            .GroupBy(t => t.PostingDate.Date)
            .Select(group => new
            {
                Date = group.Key,
                TotalAmount = group.Sum(t => -t.Amount)
            }).ToList();
        
        var auxBalance = account.Balance;
        var y = new decimal[transactions.Count];
        var x = new int[transactions.Count];

        for (int i = transactions.Count - 1; i >= 0; i--)
        {
            y[i] = auxBalance;
            auxBalance += transactions[i].TotalAmount;
            x[i] = (int)(transactions[i].Date - DateTime.UtcNow.Date).TotalDays;
        }
        return new GraphData(x, y, account.Color);
    });

    public static Task AccountsGraphData(List<Account> accounts, WpfPlot plot) => Task.Run(async () =>
    {
        var tasks = accounts.Select(AccountGraphData).ToArray();
        var results = await Task.WhenAll(tasks);

        foreach (var data in results)
            plot.Plot.Add.Scatter(data.X, data.Y, new Color(data.Color));
        
        plot.Refresh();
    });
}