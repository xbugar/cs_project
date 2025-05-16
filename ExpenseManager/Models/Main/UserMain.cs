using CommunityToolkit.Mvvm.ComponentModel;

namespace ExpenseManager.Models.Main;

public class UserMain : ObservableObject
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Account> Accounts { get; set; }
}