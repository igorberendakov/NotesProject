using System.ComponentModel.DataAnnotations;

namespace NotesApp.Infrastructure.Dtos
{
    public record TagViewDto(Guid Id, string Text);
    public record TagCreateDto([Required][MaxLength(30)] string Title, [Required][MaxLength(200)] string Text);
    public record TagUpdateDto([Required] Guid Id, [MaxLength(30)] string Title, [MaxLength(200)] string Text);
}
