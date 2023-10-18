using AutoMapper;

namespace ShippeeAPI.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, RecruiterFavoriteDto>()
            .ForMember(
                dest => dest.id,
                opt => opt.MapFrom(x => $"{x.id}")
            )
            .ForMember(
                 dest => dest.surname,
                opt => opt.MapFrom(x =>  $"{x.surname}")
            )
            .ForMember(
                 dest => dest.firstname,
                opt => opt.MapFrom(x =>  $"{x.firstname}")
            )
            .ForMember(
                 dest => dest.email,
                opt => opt.MapFrom(x =>  $"{x.email}")
            )
            .ForMember(
                 dest => dest.picture,
                opt => opt.MapFrom(x =>  $"{x.picture}")
            )
            .ForMember(
                 dest => dest.is_online,
                opt => opt.MapFrom(x =>  $"{x.is_online}")
            );

        CreateMap<User, StudentFavoriteDto>()
            .ForMember(
                dest => dest.id,
                opt => opt.MapFrom(x => $"{x.id}")
            )
            .ForMember(
                 dest => dest.surname,
                opt => opt.MapFrom(x =>  $"{x.surname}")
            )
            .ForMember(
                 dest => dest.firstname,
                opt => opt.MapFrom(x =>  $"{x.firstname}")
            )
            .ForMember(
                 dest => dest.cp,
                opt => opt.MapFrom(x =>  $"{x.cp}")
            )
            .ForMember(
                 dest => dest.email,
                opt => opt.MapFrom(x =>  $"{x.email}")
            )
            .ForMember(
                 dest => dest.picture,
                opt => opt.MapFrom(x =>  $"{x.picture}")
            )
            .ForMember(
                 dest => dest.is_online,
                opt => opt.MapFrom(x =>  $"{x.is_online}")
            );

        CreateMap<StudentCreateDto, User>()
            .ForMember(
                 dest => dest.surname,
                opt => opt.MapFrom(x =>  $"{x.surname}")
            )
            .ForMember(
                 dest => dest.firstname,
                opt => opt.MapFrom(x =>  $"{x.firstname}")
            )
            .ForMember(
                 dest => dest.email,
                opt => opt.MapFrom(x =>  $"{x.email}")
            )
            .ForMember(
                 dest => dest.password,
                opt => opt.MapFrom(x =>  $"{x.password}")
            )
            .ForMember(
                 dest => dest.description,
                opt => opt.MapFrom(x =>  $"{x.description}")
            )
            .ForMember(
                 dest => dest.web_site,
                opt => opt.MapFrom(x =>  $"{x.web_site}")
            )
            .ForMember(
                 dest => dest.cp,
                opt => opt.MapFrom(x =>  $"{x.cp}")
            )
            .ForMember(
                 dest => dest.city,
                opt => opt.MapFrom(x =>  $"{x.city}")
            )
            .ForMember(
                 dest => dest.birthday,
                opt => opt.MapFrom(x =>  $"{x.birthday}")
            )
            .ForMember(
                 dest => dest.is_conveyed,
                opt => opt.MapFrom(x =>  $"{x.is_conveyed}")
            );

        
        CreateMap<RecruiterCreateDto, User>()
            .ForMember(
                 dest => dest.surname,
                opt => opt.MapFrom(x =>  $"{x.surname}")
            )
            .ForMember(
                 dest => dest.firstname,
                opt => opt.MapFrom(x =>  $"{x.firstname}")
            )
            .ForMember(
                 dest => dest.email,
                opt => opt.MapFrom(x =>  $"{x.email}")
            )
            .ForMember(
                 dest => dest.password,
                opt => opt.MapFrom(x =>  $"{x.password}")
            )
            .ForMember(
                 dest => dest.picture,
                opt => opt.MapFrom(x =>  $"{x.picture}")
            )
            .ForMember(
                 dest => dest.id_company,
                opt => opt.MapFrom(x =>  $"{x.id_company}")
            );
    }
}