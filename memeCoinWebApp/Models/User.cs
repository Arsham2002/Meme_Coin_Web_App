using System.ComponentModel.DataAnnotations;

namespace memeCoinWebApp.Models;

public class User
{
    [Key]
    public required string PhoneNumber { set; get; }
    [StringLength(128, MinimumLength = 8)]
    public required string Password { set; get; }
    public decimal Balance { set; get; }

    // foreign keys
    public List<Transfer>? Transfers { set; get; }
    public List<Fund>? Funds { set; get; }
    public List<Message>? Messages { set; get; }
}
