using System.ComponentModel.DataAnnotations;

namespace NotesApp.Infrastructure.Dtos
{
    public record NotificationViewDto(Guid Id, DateTime TimeBinding)
    {
        public NoteViewDto Note { get; set; } = null!;
    };
    public record NotificationCreateDto([Required] Guid NoteId, [Required] DateTime TimeBinding);
    public record NotificationUpdateDto([Required] Guid Id, DateTime TimeBinding);
}