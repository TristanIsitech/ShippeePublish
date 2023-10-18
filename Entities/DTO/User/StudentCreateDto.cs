using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class StudentCreateDto
{

    public string? surname { get; set; }

    public string? firstname { get; set; }

    public string? email { get; set; }

    public string? password { get; set; }

    public string? description { get; set; }

    public string? web_site { get; set; }

    public string? cp { get; set; }

    public string? city { get; set; }

    public DateTime? birthday { get; set; } 

    public bool? is_conveyed { get; set; }
}