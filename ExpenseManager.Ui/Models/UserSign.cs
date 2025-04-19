using CommunityToolkit.Mvvm.ComponentModel;

namespace ExpenseManager.Ui.Models;

public class UserSign : ObservableObject
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}