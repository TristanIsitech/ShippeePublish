namespace ShippeeAPI;

public class RecruiterDto
{
    public bool connexion { get; set; }
    
    public Int32 id { get; set; }

    public string? surname { get; set; }

    public string? firstname { get; set; }

    public string? email { get; set; }

    public string? picture { get; set; }

    public bool? is_online { get; set; }

    public Type_User? type_user {get; set; }

    public CompanyDto? company { get; set; }

    public List<AnnoucementRecruiterDto>? annoucements {get; set; }

    public List<AnnoucementFavoriteStudentDto>? favorites {get; set; }

    public List<AnnoucementRecentRecruiterDto>? recent_announcements { get; set; }
    
    public List<ProfileChatDto>? convs { get; set; }

    public List<Recent_SearchDto>? recent_searchs { get; set; }

    public List<AnnoucementRecentRecruiterDto>? select_announcements { get; set; }

    public List<AnnoucementRecentRecruiterDto>? loc_announcements { get; set; }
    
}