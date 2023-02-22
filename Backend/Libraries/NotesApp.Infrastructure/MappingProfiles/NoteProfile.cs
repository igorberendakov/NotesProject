using AutoMapper;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Infrastructure.MappingProfiles
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteViewDto>()
                .ForMember(
                dest => dest.Tags,
                opt => opt.MapFrom(src => src.NoteTags.Select(x => x.Tag)));
            CreateMap<NoteViewDto, Note>();
            CreateMap<NoteCreateDto, Note>()
                .ForMember(
                dest => dest.NoteTags,
                opt => opt.MapFrom(src => src.TagGuids != null ? src.TagGuids.Select(x => new NoteTag { TagId = x }) : null));
            CreateMap<NoteUpdateDto, Note>()
                .ForMember(
                dest => dest.NoteTags,
                opt => opt.MapFrom(src => src.TagGuids != null ? src.TagGuids.Select(x => new NoteTag { TagId = x }) : null));
        }
    }
}