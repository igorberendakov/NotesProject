using System.ComponentModel.DataAnnotations;

namespace NotesApp.Infrastructure.Dtos
{
    public record NoteViewDto(Guid Id, string Title, string Text)
    {
        public IEnumerable<TagViewDto> Tags { get; set; } = null!;
    };
    public record NoteCreateDto([Required][MaxLength(30)] string Title, [Required][MaxLength(200)] string Text, IEnumerable<Guid>? TagGuids = default);
    public record NoteUpdateDto([Required] Guid Id, [MaxLength(30)] string Title, [MaxLength(200)] string Text, IEnumerable<Guid>? TagGuids = default);
}