using NotesApp.Domain.Abstractions;

namespace NotesApp.Domain.Entities
{
    internal class Notification : IEntity
    {
        /// <summary>
        /// Идентификатор напоминания.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Идентификатор заметки
        /// </summary>
        public Guid NoteId { get; set; }
        /// <summary>
        /// Привязка ко времени.
        /// </summary>
        public DateTime? TimeBinding { get; set; }
        /// <summary>
        /// Заметка.
        /// </summary>
        public virtual Note? Note { get; set; }
    }
}
