using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace memeCoinWebApp.Models;

public class Transfer
{
    [Key]
    public int Id { set; get; }
    public decimal Amount { set; get; }
    public DateTime Timestamp { set; get; }

    // foreign keys 
    [ForeignKey("Source")]
    public required string Source { set; get; }
    public User? SourceUser { set; get; }
    [ForeignKey("Destination")]
    public required string Destination { set; get; }
    public User? DestinationUser { set; get; }
}
