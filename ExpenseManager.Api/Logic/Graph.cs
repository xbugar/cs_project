using ExpenseManager.Api.Database;
using ScottPlot;

namespace ExpenseManager.Api.Logic;

public static class Graph
{
    public record GraphData(
        double[] XData,
        double[] YData,
        Color Color
    );

    // public static Task<GraphData> AccountGraphData(List<Account> account) => Task.Run(() =>
    // {
    //     int day = DateTime.Now.Day;
    //
    //     return new();
    // });

    // public static async Task<GraphData[]> AccountsGraphData(List<Account> account)
    // {
    //     
    // }
}