namespace ShippeeAPI;

public class AnnoucementRecruiterDto
{
    public Int32 id { get; set; }

    public string? title { get; set; }

    public string? description { get; set; }

    public DateTime? publish_date { get; set; }

    public Annoucement_State? status { get; set; }

    public string? naf_division_title { get; set; }

    public string? diplome { get; set; }

    public Dictionary<Int32, string>? qualifications { get; set; }
}