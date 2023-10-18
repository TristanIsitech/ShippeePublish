namespace ShippeeAPI;

public class ProfileChatDto
{
    public Int32? id_people { get; set; }
    public string? user_surname { get; set; }

    public string? user_firstname { get; set; }

    public string? user_picture { get; set; }

    public bool? user_is_online { get; set; }

    public List<ChatDto> chat { get; set; }
}