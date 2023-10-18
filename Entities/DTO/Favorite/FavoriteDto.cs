using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class FavoriteDto
{
    public int id_annoucement { get; set; }
    public int id_user { get; set; }
}