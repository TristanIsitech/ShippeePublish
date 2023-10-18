using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class User
{

    [Key]
    public Int32 id { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? surname { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? firstname { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? email { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? password { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? picture { get; set; }

    public bool? is_online { get; set; }

    public Type_User? Type_User { get; set; }
    
    [ForeignKey("Type_User")]
    public Int32? id_type_user { get; set; }

    public string? description { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? web_site { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? cv { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? cp { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? city { get; set; }

    public DateTime? birthday { get; set; } 
    
    public bool? is_conveyed { get; set; }

    public List<Student_Skill>? skills { get; set; }

    public Company? Company { get; set; }
    
    [ForeignKey("Company")]
    public Int32? id_company { get; set; }

    public List<Favorite>? favorites_annoucements { get; set; }
    public List<Recent>? recents_annoucements { get; set; }

}