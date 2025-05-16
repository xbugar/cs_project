using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManager.Database;

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    
    [Required]
    public TransactionType Type { get; set; } = TransactionType.Other;
    
    public enum TransactionType
    {
        Income,
        Savings,
        Food,
        Entertainment,
        Rent,
        Other
    }
    
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