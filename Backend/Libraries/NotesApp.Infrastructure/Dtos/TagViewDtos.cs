namespace NotesApp.Infrastructure.Dtos
{
    public record TagViewDto(Guid Id, string Text);
    public record TagCreateDto(string Text);
    public record TagUpdateDto(Guid Id, string Text);
}
