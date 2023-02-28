namespace NotesApp.Infrastructure.Configurations.Authentication.Models
{
    public record LoginResult(bool Success, string? Token, string? Message);
}