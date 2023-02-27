namespace NotesApp.Infrastructure.Configurations.Authentication.Models
{
    public class JwtOptions
    {
        public string Token { get; init; } = null!;
        public string SecretKey { get; init; } = null!;
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
        public bool Success { get; init; }
    }
}
