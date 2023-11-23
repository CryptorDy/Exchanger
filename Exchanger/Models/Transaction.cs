using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exchanger.Models;

public class Transaction
{
    [Key]
    public Guid Id { get; set; }

    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

    [Required(ErrorMessage = "CurrencyCode is required")]
    [RegularExpression("^[A-Za-z]{3}$", ErrorMessage = "CurrencyCode must contain only English letters")]
    public string CurrencyCode { get; set; }

    [Required]
    public Guid SenderUserid { get; set; }

    [Required]
    public Guid RecipientUserId { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    [Column(TypeName = "decimal(,8)")]
    [Range(1, double.MaxValue, ErrorMessage = "Amount must be a non-negative number")]
    public decimal Amount { get; set; }

    public TransactionType Type { get; set; } = TransactionType.TransferToUser;

    public Guid? ExchangeId { get; set; } = null;

    [NotMapped]
    public static Guid System = new Guid("aa627267-e2db-420f-8ee1-f996c10d212f");

    [NotMapped]
    public static Guid FeeRecipient = new Guid("baf0f305-bfbf-42b3-a99d-444631737ad1");

    [NotMapped]
    public static Guid Exchanger = new Guid("229444d6-fe9a-4f4e-b45d-27a22625406f");
}
