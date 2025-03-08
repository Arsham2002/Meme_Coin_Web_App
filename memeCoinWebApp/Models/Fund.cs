using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace memeCoinWebApp.Models;

public class Fund
{
    [Key]
    public int Id { set; get; }
    public decimal Amount { set; get; }
    public DateTime Timestamp { set; get; }

    // foreign keys
    [ForeignKey("UserPhoneNumber")]
    public required string UserPhoneNumber { set; get; }
    public User? User { set; get; }
}
