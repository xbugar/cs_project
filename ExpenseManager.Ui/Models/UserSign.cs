#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using CommunityToolkit.Mvvm.ComponentModel;

namespace ExpenseManager.Ui.Models;

public class UserSign : ObservableObject
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}