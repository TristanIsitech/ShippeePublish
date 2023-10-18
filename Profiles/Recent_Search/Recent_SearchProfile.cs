using AutoMapper;

namespace ShippeeAPI.Profiles;

public class Recent_SearchProfile : Profile
{
    public Recent_SearchProfile()
    {
        CreateMap<Recent_Search, Recent_SearchDto>()
            .ForMember(
                dest => dest.text,
                opt => opt.MapFrom(x => $"{x.text}")
            );
    }
}