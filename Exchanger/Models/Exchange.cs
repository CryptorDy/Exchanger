using System;
using System.ComponentModel.DataAnnotations;

public class Exchange
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "DateCreated is required")]
    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

    [Required(ErrorMessage = "CurrencyCodeFrom is required")]
    [RegularExpression("^[A-Za-z]{3}$", ErrorMessage = "CurrencyCodeFrom must contain only English letters")]
    public string CurrencyCodeFrom { get; set; }

    [Required(ErrorMessage = "CurrencyCodeTo is required")]
    [RegularExpression("^[A-Za-z]{3}$", ErrorMessage = "CurrencyCodeTo must contain only English letters")]
    public string CurrencyCodeTo { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    [Range(1, double.MaxValue, ErrorMessage = "Amount must be a non-negative number")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(1, double.MaxValue, ErrorMessage = "Price must be a non-negative number")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Fee is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Fee must be a non-negative number")]
    public decimal Fee { get; set; }
}
