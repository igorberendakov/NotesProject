namespace NotesApp.Domain.Entities
{
    public class NoteTag
    {
        /// <summary>
        /// Идентификатор заметки.
        /// </summary>
        public Guid NoteId { get; set; }
        /// <summary>
        /// Идентификатор тэга.
        /// </summary>
        public Guid TagId { get; set; }
        /// <summary>
        /// Заметка.
        /// </summary>
        public virtual Note Note { get; set; } = null!;
        /// <summary>
        /// Тэг.
        /// </summary>
        public virtual Tag Tag { get; set; } = null!;
    }
}