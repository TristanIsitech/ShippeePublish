using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class Annoucement
{
    [Key]
    public Int32 id { get; set; }

    public User? User { get; set; }
    
    [ForeignKey("User")]
    public Int32? id_user { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? title { get; set; }

    public string? description { get; set; }

    public DateTime? publish_date { get; set; }

    public Type_User? Type_User { get; set; }
    
    [ForeignKey("Type_User")]
    public Int32? id_type { get; set; }

    public Annoucement_State? Annoucement_State { get; set; }
    
    [ForeignKey("Annoucement_State")]
    public Int32? id_status { get; set; }

    public Naf_Division? Naf_Division { get; set; }
    
    [ForeignKey("Naf_Division")]
    public Int32? id_naf_division { get; set; }

    public Diplome? Diplome { get; set; }

    [ForeignKey("Diplome")]
    public Int32? id_diplome { get; set; }
    
    public List<Qualification>? skills { get; set; }
    public List<Favorite>? favorites_users { get; set; }
    public List<Recent>? recents_visits { get; set; }
}