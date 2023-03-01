using FluentValidation;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Infrastructure.Validation.Validators
{
    public class NoteTagValidator : AbstractValidator<NoteTagDto>
    {
        public NoteTagValidator()
        {
            RuleFor(x => x.NoteId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.TagId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
        }
    }
}
