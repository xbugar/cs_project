using CommunityToolkit.Mvvm.ComponentModel;

namespace ExpenseManager.Ui.Models;

public abstract class Transaction  : ObservableObject
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PostingDate { get; set; }
    public string Description { get; set; }
}