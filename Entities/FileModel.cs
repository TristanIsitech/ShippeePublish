using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippeeAPI;

public class FileModel
{
    public IFormFile? file_cv { get; set; }
    public IFormFile? file_picture { get; set; }
}