using CommunityToolkit.Mvvm.ComponentModel;

namespace ExpenseManager.Ui.Models.Main;

public class UserMain : ObservableObject
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public List<Account> Accounts { get; set; }
}