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
            CreateMap<NoteCreateDto, Note>();
            CreateMap<NoteUpdateDto, Note>();
        }
    }
}
