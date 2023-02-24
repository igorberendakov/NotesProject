using AutoMapper;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Infrastructure.Mapping.Profiles
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
            CreateMap<NotificationUpdateDto, Notification>()
                .ForMember(dest=>dest.NoteId,opt=>opt.Ignore());
        }
    }
}