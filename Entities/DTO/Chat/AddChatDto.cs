namespace ShippeeAPI;

public class AddChatDto
{
    public int? id_sender { get; set; }
    public int? id_recipient { get; set; }
    public string? content { get; set; }
}