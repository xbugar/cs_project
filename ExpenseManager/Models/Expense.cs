namespace ExpenseManager.Models;

public class Expense : Transaction
{
    public ExpenseType Type { get; set; } = ExpenseType.Other;
    
    public enum ExpenseType
    {
        Food,
        Entertainment,
        Rent,
        Other
    }
}
