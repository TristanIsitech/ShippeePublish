using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class Recent_Search
{
    [Key]
    public Int32 id { get; set; }

    public User? User { get; set; }

    [ForeignKey("User")]
    public Int32? id_user { get; set; }

    public string? text { get; set; }
}