namespace ExpenseManager.Ui.Models;

public class Income : Transaction
{
    public IncomeType Type { get; set; } = IncomeType.Salary;
    
    public enum IncomeType
    {
        Salary,
        Gift,
    };
}
