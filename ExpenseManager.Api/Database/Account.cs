using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManager.Api.Database;

public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    
    [Required]
    [StringLength(64, MinimumLength = 1)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(7, MinimumLength = 7)]
    public string Color { get; set; } = null!;
    
    [Required]
    [StringLength(256)]
    public string Description { get; set; } = "";
    
    [Required]
    public List<Transaction> Transactions { get; set; } = [];

    [Required]
    public decimal Balance { get; set; } = 0m;
}