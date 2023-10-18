using AutoMapper;

namespace ShippeeAPI.Profiles;

public class AnnoucementProfile : Profile
{
    public AnnoucementProfile()
    {
        CreateMap<Annoucement, AnnoucementRecruiterDto>()
            .ForMember(
                dest => dest.id,
                opt => opt.MapFrom(x => $"{x.id}")
            )
            .ForMember(
                 dest => dest.title,
                opt => opt.MapFrom(x =>  $"{x.title}")
            )
            .ForMember(
                dest => dest.description,
                opt => opt.MapFrom(x => $"{x.description}")
            )
            .ForMember(
                dest => dest.publish_date,
                opt => opt.MapFrom(x => $"{x.publish_date}")
            ); 
            
        CreateMap<Annoucement, AnnoucementStudentDto>()
            .ForMember(
                dest => dest.id,
                opt => opt.MapFrom(x => $"{x.id}")
            )
            .ForMember(
                 dest => dest.title,
                opt => opt.MapFrom(x =>  $"{x.title}")
            )
            .ForMember(
                dest => dest.description,
                opt => opt.MapFrom(x => $"{x.description}")
            )
            .ForMember(
                dest => dest.publish_date,
                opt => opt.MapFrom(x => $"{x.publish_date}")
            );

        CreateMap<Annoucement, AnnoucementFavoriteRecruiterDto>()
            .ForMember(
                dest => dest.id,
                opt => opt.MapFrom(x => $"{x.id}")
            )
            .ForMember(
                 dest => dest.title,
                opt => opt.MapFrom(x =>  $"{x.title}")
            )
            .ForMember(
                dest => dest.description,
                opt => opt.MapFrom(x => $"{x.description}")
            )
            .ForMember(
                dest => dest.publish_date,
                opt => opt.MapFrom(x => $"{x.publish_date}")
            ); 

        CreateMap<Annoucement, AnnoucementFavoriteStudentDto>()
            .ForMember(
                dest => dest.id,
                opt => opt.MapFrom(x => $"{x.id}")
            )
            .ForMember(
                 dest => dest.title,
                opt => opt.MapFrom(x =>  $"{x.title}")
            )
            .ForMember(
                dest => dest.description,
                opt => opt.MapFrom(x => $"{x.description}")
            )
            .ForMember(
                dest => dest.publish_date,
                opt => opt.MapFrom(x => $"{x.publish_date}")
            ); 

        CreateMap<Annoucement, AnnoucementRecentStudentDto>()
            .ForMember(
                dest => dest.id,
                opt => opt.MapFrom(x => $"{x.id}")
            )
            .ForMember(
                 dest => dest.title,
                opt => opt.MapFrom(x =>  $"{x.title}")
            )
            .ForMember(
                dest => dest.description,
                opt => opt.MapFrom(x => $"{x.description}")
            )
            .ForMember(
                dest => dest.publish_date,
                opt => opt.MapFrom(x => $"{x.publish_date}")
            ); 

        CreateMap<Annoucement, AnnoucementRecentRecruiterDto>()
            .ForMember(
                dest => dest.id,
                opt => opt.MapFrom(x => $"{x.id}")
            )
            .ForMember(
                 dest => dest.title,
                opt => opt.MapFrom(x =>  $"{x.title}")
            )
            .ForMember(
                dest => dest.description,
                opt => opt.MapFrom(x => $"{x.description}")
            )
            .ForMember(
                dest => dest.publish_date,
                opt => opt.MapFrom(x => $"{x.publish_date}")
            ); 
    }
}