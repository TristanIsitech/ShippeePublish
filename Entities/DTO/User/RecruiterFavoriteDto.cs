namespace ShippeeAPI;

public class RecruiterFavoriteDto
{
    public Int32 id { get; set; }

    public string? surname { get; set; }

    public string? firstname { get; set; }

    public string? email { get; set; }

    public string? picture { get; set; }

    public bool? is_online { get; set; }

    public CompanyDto? company { get; set; }
}