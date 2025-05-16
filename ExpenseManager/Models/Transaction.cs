using CommunityToolkit.Mvvm.ComponentModel;

namespace ExpenseManager.Models;

public class Transaction  : ObservableObject
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PostingDate { get; set; }
    public string Description { get; set; }
}