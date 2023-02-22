using AutoMapper;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Infrastructure.MappingProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagViewDto>();
            CreateMap<TagCreateDto, Tag>();
            CreateMap<TagUpdateDto, Tag>();
        }
    }
}
