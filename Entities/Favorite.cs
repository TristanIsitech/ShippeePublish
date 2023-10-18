namespace ShippeeAPI;

public class Favorite
{
    public Int32 id_user { get; set; }
    public User? User { get; set; }

    public Int32 id_annoucement { get; set; }
    public Annoucement? Annoucement { get; set; }
}