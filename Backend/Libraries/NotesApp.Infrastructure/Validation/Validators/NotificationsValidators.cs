using FluentValidation;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Infrastructure.Validation.Validators
{
    public class NotificationCreateValidator : AbstractValidator<NotificationCreateDto>
    {
        public NotificationCreateValidator()
        {
            RuleFor(x => x.NoteId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.TimeBinding)
                .NotNull()
                .NotEmpty()
                .GreaterThan(DateTime.UtcNow);
        }
    }

    public class NotificationUpdateValidator : AbstractValidator<NotificationUpdateDto>
    {
        public NotificationUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.TimeBinding)
                .NotNull()
                .NotEmpty()
                .GreaterThan(DateTime.UtcNow);
        }
    }
}