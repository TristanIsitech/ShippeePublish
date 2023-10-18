using AutoMapper;

namespace ShippeeAPI.Profiles;

public class NafProfile : Profile
{
    public NafProfile()
    {
        CreateMap<Naf_Division, NafDivisonDto>()
            .ForMember(
                dest => dest.id,
                opt => opt.MapFrom(x => $"{x.id}")
            )
            .ForMember(
                dest => dest.title,
                opt => opt.MapFrom(x => $"{x.title}")
            );;

    }
}