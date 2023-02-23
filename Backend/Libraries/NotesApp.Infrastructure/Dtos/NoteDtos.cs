using System.ComponentModel.DataAnnotations;

namespace NotesApp.Infrastructure.Dtos
{
    public record NoteViewDto(Guid Id, string Title, string Text)
    {
        public IEnumerable<TagViewDto> Tags { get; set; } = null!;
    };
    public record NoteCreateDto(string Title, string Text, IEnumerable<Guid>? TagGuids = default);
    public record NoteUpdateDto(Guid Id, string Title, string Text, IEnumerable<Guid>? TagGuids = default);
}