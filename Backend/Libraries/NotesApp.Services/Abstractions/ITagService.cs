using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Services.Abstractions
{
    public interface ITagService
    {
        /// <summary>
        /// Получение списка DTO тэгов с применением запроса OData.
        /// </summary>
        /// <returns>Список DTO тэгов.</returns>
        IQueryable<TagViewDto> GetQueryable();
        /// <summary>
        /// Создание нового тэга.
        /// </summary>
        /// <param name="tagCreateDto">Модель тэга с необходимыми для создания тэга данными.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>Идентификатор созданного тэга.</returns>
        Task<Guid> CreateTagAsync(TagCreateDto tagCreateDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Изменение тэга.
        /// </summary>
        /// <param name="noteUpdateDto">Модель тэга с данными, необходимыми для внесения изменений.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>true - если были внесены изменения, false -  если изменений не было.</returns>
        Task<bool> UpdateTagAsync(TagUpdateDto tagUpdateDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удаление тэга.
        /// </summary>
        /// <param name="id">Идентификатор тэга.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>true - если произведено удаление, false - если заметки не существует.</returns>
        Task<bool> DeleteTagAsync(Guid id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Добавление привязки тэга к заметке.
        /// </summary>
        /// <param name="noteTagDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AddTagToNote(NoteTagDto noteTagDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удаление привязки тэга к заметке.
        /// </summary>
        /// <param name="noteTagDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> RemoveTagFromNote(NoteTagDto noteTagDto, CancellationToken cancellationToken = default);
    }
}