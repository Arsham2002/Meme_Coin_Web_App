using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace memeCoinWebApp.Models;

public class Fund
{
    [Key]
    public int Id { set; get; }
    public int Amount { set; get; }
    public DateTime Timestamp { set; get; }

    // foreign keys
    [ForeignKey("UserPhoneNumber")]
    public string UserPhoneNumber { set; get; }
    public User? User { set; get; }
}
