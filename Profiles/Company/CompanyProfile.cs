using AutoMapper;

namespace ShippeeAPI.Profiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(
                dest => dest.siren,
                opt => opt.MapFrom(x => $"{x.siren}")
            )
            .ForMember(
                 dest => dest.name,
                opt => opt.MapFrom(x =>  $"{x.name}")
            )
            .ForMember(
                dest => dest.city,
                opt => opt.MapFrom(x => $"{x.city}")
            )
            .ForMember(
                dest => dest.cp,
                opt => opt.MapFrom(x => $"{x.cp}")
            )
            .ForMember(
                dest => dest.picture,
                opt => opt.MapFrom(x => $"{x.picture}")
            );

        CreateMap<CompanyCreateDto, Company>()
            .ForMember(
                dest => dest.siren,
                opt => opt.MapFrom(x => $"{x.siren}")
            )
            .ForMember(
                 dest => dest.name,
                opt => opt.MapFrom(x =>  $"{x.name}")
            )
            .ForMember(
                dest => dest.id_naf,
                opt => opt.MapFrom(x => $"{x.id_naf}")
            )
            .ForMember(
                dest => dest.picture,
                opt => opt.MapFrom(x => $"{x.picture}")
            )
            .ForMember(
                dest => dest.street,
                opt => opt.MapFrom(x => $"{x.street}")
            )
            .ForMember(
                dest => dest.cp,
                opt => opt.MapFrom(x => $"{x.cp}")
            )
            .ForMember(
                 dest => dest.city,
                opt => opt.MapFrom(x =>  $"{x.city}")
            )
            .ForMember(
                dest => dest.legal_form,
                opt => opt.MapFrom(x => $"{x.legal_form}")
            )
            .ForMember(
                dest => dest.id_effective,
                opt => opt.MapFrom(x => $"{x.id_effective}")
            )
            .ForMember(
                dest => dest.web_site,
                opt => opt.MapFrom(x => $"{x.web_site}")
            );

        CreateMap<Company, CompanySelectDto>()
            .ForMember(
                dest => dest.siren,
                opt => opt.MapFrom(x => $"{x.siren}")
            )
            .ForMember(
                 dest => dest.name,
                opt => opt.MapFrom(x =>  $"{x.name}")
            );
    }
}