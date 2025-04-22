using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Api.Database;

public class Expense : Transaction
{
    [Required]
    public ExpenseType Type { get; private set; } = ExpenseType.Other;
    
    public enum ExpenseType
    {
        Food,
        Entertainment,
        Rent,
        Other
    };
}