using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Api.Database;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [Required]
    [EmailAddress]
    [StringLength(254)]
    public string Email { get; init; } = null!;

    [Required]
    public string PasswordHash { get; init; } = null!;

    [Required]
    [StringLength(64)]
    public string FirstName { get; init; } = null!;

    [Required]
    [StringLength(64)]
    public string LastName { get; init; } = null!;
    
    [Required]
    public List<Account> Accounts { get; set; } = new ();
}