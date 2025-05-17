using System.IO;
using System.Windows;
using System.Windows.Controls;
using CSharpFunctionalExtensions;
using ExpenseManager.Database;

namespace ExpenseManager.Services;

public class Files
{
    private static Task<Result<List<Transaction>>> ImportTransactions(string file, Account account) => Task.Run(async () =>
    {
        var result = new List<Transaction>();
        if (!file.EndsWith(".csv"))
            return Result.Failure<List<Transaction>>("Invalid file format .csv required");

        var lines = await File.ReadAllLinesAsync(file);

        var correctLines = lines
            .Skip(1)
            .Select(l => l.Split("\t"))
            .Where(l => l.Length == 10);

        foreach (var line in correctLines)
        {
            try
            {
                result.Add(new()
                {
                    AccountId = account.Id,
                    PostingDate = DateTime.Parse(line[2]),
                    Amount = decimal.Parse(line[5]),
                    Description = line[4],
                });
            }
            catch (Exception)
            {
                return Result.Failure<List<Transaction>>("Invalid file format");
            }
        }
        return result;
    });

    public static async Task<List<Transaction>> ProcessDroppedFile(string[] files, Account account)
    {
        var tasks = files
            .Select(f => ImportTransactions(f, account))
            .ToArray();
        var results = await Task.WhenAll(tasks);
        var result = new List<Transaction>();

        foreach (var res in results)
        {
            if (res.IsFailure)
            {
                MessageBox.Show(res.Error);
                continue;
            }

            result.AddRange(res.Value);
        }

        await using var ctx = new AppDbContext();
        try
        {
            await ctx.Transactions.AddRangeAsync(result);
            await ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return [];
        }

        return result;
    }
}