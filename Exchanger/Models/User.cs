using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exchanger.Models;

public class User
{
    public User(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Balances = new List<Balance>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [NotMapped]
    public List<Balance> Balances { get; set; }
}
