namespace NotesApp.Infrastructure.Validation
{
    public class ErrorResponse
    {
        public List<ValidationErrorModel> Errors { get; set; } = null!;
    }
}
