using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class Company
{
    [Key]
    public Int32 siren { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? name { get; set; }

    public Naf_Section? Naf_Section { get; set; }
    
    [ForeignKey("Naf_Section")]
    public Int32? id_naf { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? picture { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? street { get; set; }
    
    [Column(TypeName = "varchar(255)")]
    public string? cp { get; set; }
    
    [Column(TypeName = "varchar(255)")]
    public string? city { get; set; }
    
    [Column(TypeName = "varchar(255)")]
    public string? legal_form { get; set; }
    
    public Effective? Effective { get; set; }
    
    [ForeignKey("Effective")]
    public Int32? id_effective { get; set; }
    
    [Column(TypeName = "varchar(255)")]
    public string? web_site { get; set; } 

    public bool? payment { get; set; }

}