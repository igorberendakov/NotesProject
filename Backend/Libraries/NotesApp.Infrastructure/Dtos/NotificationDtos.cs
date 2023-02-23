using System.ComponentModel.DataAnnotations;

namespace NotesApp.Infrastructure.Dtos
{
    public record NotificationViewDto(Guid Id, DateTime TimeBinding)
    {
        public NoteViewDto Note { get; set; } = null!;
    };
    public record NotificationCreateDto(Guid NoteId, DateTime TimeBinding);
    public record NotificationUpdateDto(Guid Id, DateTime TimeBinding);
}