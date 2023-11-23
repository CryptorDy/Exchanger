using System.ComponentModel.DataAnnotations;

namespace Exchanger.Models;

public class Currency
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [RegularExpression("^[A-Za-z]*$", ErrorMessage = "Name must contain only English letters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Code is required")]
    [RegularExpression("^[A-Za-z]{3}$", ErrorMessage = "Code must contain only English letters")]
    public string Code { get; set; }
}
