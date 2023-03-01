using AutoMapper;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Infrastructure.Mapping.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagViewDto>();
            CreateMap<TagViewDto, Tag>();
            CreateMap<TagViewDto, NoteTag>()
                .ForMember(
                x => x.TagId, 
                opt => opt.MapFrom(x => x.Id))
                .ForMember(
                x => x.NoteId, 
                opt => opt.Ignore());
            CreateMap<TagCreateDto, Tag>();
            CreateMap<TagUpdateDto, Tag>();
        }
    }
}
