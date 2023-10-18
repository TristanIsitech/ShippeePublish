using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class Recent
{
    public Int32 id_user { get; set; }
    public User? User { get; set; }

    public Int32 id_annoucement { get; set; }
    public Annoucement? Annoucement { get; set; }

    public DateTime consult_date { get; set; }
}