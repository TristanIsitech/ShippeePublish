using AutoMapper;

namespace ShippeeAPI.Profiles;

public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<Chat, ChatDto>()
            .ForMember(
                dest => dest.id,
                opt => opt.MapFrom(x => $"{x.id}")
            )
            .ForMember(
                 dest => dest.content,
                opt => opt.MapFrom(x =>  $"{x.content}")
            )
            .ForMember(
                dest => dest.send_time,
                opt => opt.MapFrom(x => $"{x.send_time}")
            )
            .ForMember(
                dest => dest.status,
                opt => opt.MapFrom(x => $"{x.status}")
            ); 
    }
}