using FluentValidation;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Infrastructure.Validation.Validators
{
    public class NoteCreateValidator : AbstractValidator<NoteCreateDto>
    {
        public NoteCreateValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Text)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200);
        }
    }

    public class NoteUpdateValidator : AbstractValidator<NoteUpdateDto>
    {
        public NoteUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Title)
                .MaximumLength(30);
            RuleFor(x => x.Text)
                .MaximumLength(200);
        }
    }
}
