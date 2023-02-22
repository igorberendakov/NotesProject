using AutoMapper;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Infrastructure.MappingProfiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationViewDto>();
            CreateMap<NotificationViewDto, Notification>()
                .ForMember(
                dest => dest.Note,
                opt => opt.MapFrom(src => src.Note));
            CreateMap<NotificationCreateDto, Notification>();
            CreateMap<NotificationUpdateDto, Notification>();
        }
    }
}