namespace ShippeeAPI;

public class Qualification
{
    public Int32 id_annoucement { get; set; }
    public Annoucement? Annoucement { get; set; }

    public Int32 id_skill { get; set; }
    public Skill? Skill { get; set; }
}