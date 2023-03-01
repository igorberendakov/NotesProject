using NotesApp.Domain.Abstractions;

namespace NotesApp.Domain.Entities
{
    /// <summary>
    /// Тэг.
    /// </summary>
    public class Tag : IEntity
    {
        /// <summary>
        /// Идентификатор тэга.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Текст тэга.
        /// </summary>
        public string Text { get; set; } = null!;
    }
}