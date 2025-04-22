using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Api.Database;


[Index(nameof(Name), IsUnique = true)]
public class Icon
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    
    [Required]
    [StringLength(64, MinimumLength = 1)]
    public string Name { get; init; } = null!;
    
    [Required]
    public byte[] Data { get; init; } = null!;
    
    [Required]
    public List<Account> Accounts { get; init; } = new();
}