namespace ShippeeAPI;

public class ChatDto
{
    public Int32 id { get; set; }

    public string? content { get; set; }

    public DateTime? send_time { get; set; }

    public string? status { get; set; }

    public bool? who { get; set; }
}