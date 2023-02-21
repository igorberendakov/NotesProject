using NotesApp.Domain.Abstractions;

namespace NotesApp.Domain.Entities
{
    public class Note : IEntity
    {
        /// <summary>
        /// Идентификатор заметки.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Заголовок заметки.
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// Текст заметки.
        /// </summary>
        public string Text { get; set; } = null!;
    }
}