using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class Chat
{
    [Key]
    public Int32 id { get; set; }

    public User? User { get; set; }

    [ForeignKey("User")]
    public Int32? id_sender { get; set; }

    public User? User2 { get; set; }

    [ForeignKey("User2")]
    public Int32? id_recipient { get; set; }
    
    public string? content { get; set; }

    public DateTime? send_time { get; set; }

    public Chat_State? Chat_State { get; set; }

    [ForeignKey("Chat_State")]
    public Int32? status { get; set; }
}