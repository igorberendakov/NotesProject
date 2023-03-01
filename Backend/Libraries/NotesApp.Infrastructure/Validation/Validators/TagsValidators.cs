using FluentValidation;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Infrastructure.Validation.Validators
{
    public class TagCreateValidator : AbstractValidator<TagCreateDto>
    {
        public TagCreateValidator()
        {
            RuleFor(x => x.Text)
                .NotNull()
                .NotEmpty()
                .MaximumLength(15);
        }
    }

    public class TagUpdateValidator : AbstractValidator<TagUpdateDto>
    {
        public TagUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Text)
                .MaximumLength(15);
        }
    }
}