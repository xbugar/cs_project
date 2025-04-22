using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManager.Api.Database;

public abstract class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    
    [Required]
    public decimal Amount { get; set; }

    [Required] public DateTime PostingDate { get; set; } = DateTime.UtcNow;

    [Required]
    public string Description { get; set; } = "";
    
    [Required]
    public int AccountId { get; set; }
    
    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; } = null!;
}