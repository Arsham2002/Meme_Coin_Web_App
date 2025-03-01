using System.ComponentModel.DataAnnotations;

namespace memeCoinWebApp.Models;

public class Message
{
    [Key]
    public int Id { set; get; }
    public string Sender { set; get; }
    public string Content { set; get; }
    public bool Seen { set; get; }
    // foreign keys
    public int UserId { set; get; }
    public User? User { set; get; }
}
