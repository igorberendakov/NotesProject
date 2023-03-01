using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;

namespace NotesApp.Infrastructure.Repository.Abstractions
{
    public interface ITagRepository : IRepository<Tag>
    {
        /// <summary>
        /// Добавление привязки тэга к заметке.
        /// </summary>
        /// <param name="noteId">Идентификатор заметки.</param>
        /// <param name="tagId">Идентификатор тэга.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>true - если привязка создана, false - если привязка уже существует.</returns>
        Task<bool> AddTagToNoteAsync(Guid noteId, Guid tagId, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление привязки тэга к заметке.
        /// </summary>
        /// <param name="noteId">Идентификатор заметки.</param>
        /// <param name="tagId">Идентификатор тэга.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>true - если произведено удавление, false - если привязки не существует.</returns>
        Task<bool> RemoveTagFromNoteAsync(Guid noteId, Guid tagId, CancellationToken cancellationToken);
    }
}
