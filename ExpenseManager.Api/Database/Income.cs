using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Api.Database;

public class Income : Transaction
{
    [Required] public IncomeType Type { get; private set; } = IncomeType.Salary;
    
    public enum IncomeType
    {
        Salary,
        Gift,
    };
}