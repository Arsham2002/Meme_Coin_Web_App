using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace memeCoinWebApp.Models;

public class Message
{
    [Key]
    public int Id { set; get; }
    public required string Sender { set; get; }
    public required string Content { set; get; }
    public bool Seen { set; get; }
    public DateTime Timestamp { set; get; }
    // foreign keys
    [ForeignKey("UserPhoneNumber")]
    public required string UserPhoneNumber { set; get; }
    public User? User { set; get; }
}
