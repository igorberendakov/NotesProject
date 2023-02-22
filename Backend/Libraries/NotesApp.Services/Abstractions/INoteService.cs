using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Services.Abstractions
{
    public interface INoteService
    {
        /// <summary>
        /// Получение списка DTO заметок с применением запроса OData.
        /// </summary>
        /// <returns>Список DTO заметок.</returns>
        IQueryable<NoteViewDto> GetQueryable();
        /// <summary>
        /// Создание новой заметки.
        /// </summary>
        /// <param name="noteCreateDto">DTO объект с необходимыми для создвния заметки данными.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>Идентификатор созданной заметки.</returns>
        Task<Guid> CreateNoteAsync(NoteCreateDto noteCreateDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Изменение заметки.
        /// </summary>
        /// <param name="noteUpdateDto">Модель заметки с данными, необходимыми для внесения изменений.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>true - если были внесены изменения, false -  если изменений не было.</returns>
        Task<bool> UpdateNoteAsync(NoteUpdateDto noteUpdateDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удаление заметки.
        /// </summary>
        /// <param name="id">Идентификатор заметки.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>true - если произведено удаление, false - если заметки не существует.</returns>
        Task<bool> DeleteNoteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
