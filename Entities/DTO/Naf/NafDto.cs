using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class NafDto
{
    public int id_naf_section { get; set; }
    public string? name_naf_section { get; set; }

    public List<NafDivisonDto>? naf_division { get; set; }
    
}