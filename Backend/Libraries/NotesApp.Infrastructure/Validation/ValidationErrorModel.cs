namespace NotesApp.Infrastructure.Validation
{
    public class ValidationErrorModel
    {
        public string PropertyName { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}