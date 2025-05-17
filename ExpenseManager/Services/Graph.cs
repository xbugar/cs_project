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
            .Where(trans => trans.AccountId == account.Id)
            .GroupBy(t => t.PostingDate.Date)
            .Select(group => new
            {
                Date = group.Key,
                TotalAmount = group.Sum(t => t.Amount)
            })
            .OrderBy(t => t.Date)
            .Take(30)
            .ToList();
    
        if (!transactions.Any())
            return new GraphData([], [], account.Color);
        
        var runningBalance = account.Balance;
        for (int i = transactions.Count - 1; i >= 0; i--)
        {
            runningBalance -= transactions[i].TotalAmount;
        }
    
        var y = new decimal[transactions.Count + 1];
        var x = new int[transactions.Count + 1];
    
        y[0] = runningBalance;
        x[0] = (int)(transactions.First().Date - DateTime.UtcNow.Date).TotalDays;
    
        for (int i = 0; i < transactions.Count; i++)
        {
            runningBalance += transactions[i].TotalAmount;
            y[i + 1] = runningBalance;
            x[i + 1] = (int)(transactions[i].Date - DateTime.UtcNow.Date).TotalDays;
        }
    
        return new GraphData(x, y, account.Color);
    });

    public static Task AccountsGraphData(List<Account> accounts, WpfPlot plot) => Task.Run(async () =>
    {
        var tasks = accounts.Select(AccountGraphData).ToArray();
        var results = await Task.WhenAll(tasks);

        foreach (var data in results)
        {
            var scatter = plot.Plot.Add.Scatter(data.X, data.Y, FromString(data.Color));
            scatter.LineWidth = 2;
            
            plot.Plot.Axes.AutoScale();
            plot.Refresh();
        }
        
    });
    
    
    private static Color FromString(string color)
    {
        return color switch
        {
            "Red" => Color.FromHex("#FF0000"),
            "Green" => Color.FromHex("#00FF00"),
            "Blue" => Color.FromHex("#0000FF"),
            "Yellow" => Color.FromHex("#FFFF00"),
            "Purple" => Color.FromHex("#800080"),
            "Orange" => Color.FromHex("#FFA500"),
            _ => Color.FromHex("#000000")
        };
    }
}