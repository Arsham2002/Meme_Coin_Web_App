using System.ComponentModel.DataAnnotations;

namespace memeCoinWebApp.Models;

public class Fund
{
    [Key]
    public int Id { set; get; }
    public int Amount { set; get; }
    public DateTime Timestamp { set; get; }

    // foreign keys
    public int UserId { set; get; }
    public User? User { set; get; }
}
