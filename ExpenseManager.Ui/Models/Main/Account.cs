using System.Drawing;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ExpenseManager.Ui.Models.Main;

public class Account : ObservableObject
{
    public int? Id { get; set; }
    public string Color { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public decimal Balance { get; set; }
}
