using NotesApp.Domain.Abstractions;

namespace NotesApp.Domain.Entities
{
    public class Note : IEntity
    {
        public Note() 
        {
            NoteTags = new List<NoteTag>();
        }

        /// <summary>
        /// Идентификатор заметки.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Заголовок заметки.
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// Текст заметки.
        /// </summary>
        public string Text { get; set; } = null!;
        /// <summary>
        /// Тэги, связанные с заметкой.
        /// </summary>
        public virtual ICollection<NoteTag> NoteTags { get; set; }
    }
}