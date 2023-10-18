using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class CompanyCreateDto
{

    public Int32? siren { get; set; }

    public string? name { get; set; }

    public Int32? id_naf { get; set; }

    public string? picture { get; set; }

    public string? street { get; set; }

    public string? cp { get; set; }

    public string? city { get; set; }

    public string? legal_form { get; set; }

    public Int32? id_effective { get; set; }

    public string? web_site { get; set; }

}