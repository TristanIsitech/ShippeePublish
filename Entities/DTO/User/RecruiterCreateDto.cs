using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class RecruiterCreateDto
{

    public string? surname { get; set; }

    public string? firstname { get; set; }

    public string? email { get; set; }

    public string? password { get; set; }

    public string? picture { get; set; }

    public Int32? id_company { get; set; }
}