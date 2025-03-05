using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace memeCoinWebApp.Models;

public class Transfer
{
    [Key]
    public int Id { set; get; }
    public string Source { set; get; }
    public string Destination { set; get; }
    public decimal Amount { set; get; }
    public DateTime Timestamp { set; get; }

    // foreign keys
    [ForeignKey("UserPhoneNumber")]
    public string UserPhoneNumber { set; get; }
    public User? User { set; get; }
}
