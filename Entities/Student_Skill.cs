namespace ShippeeAPI;

public class Student_Skill
{
    public Int32 id_user { get; set; }
    public User? User { get; set; }

    public Int32 id_skill { get; set; }
    public Skill? Skill { get; set; }
}