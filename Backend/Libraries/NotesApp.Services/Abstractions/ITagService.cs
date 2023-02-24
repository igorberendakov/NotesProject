﻿using NotesApp.Infrastructure.Dtos;

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
        /// <returns>true - если произведено удаление, false - если тэга не существует.</returns>
        Task<bool> DeleteTagAsync(Guid id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Добавление привязки тэга к заметке.
        /// </summary>
        /// <param name="noteTagDto">Модель привязки тэга к заметке.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>true - привязка произведена успешно, false - привязка уже существует.</returns>
        Task<bool> AddTagToNoteAsync(NoteTagDto noteTagDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удаление привязки тэга к заметке.
        /// </summary>
        /// <param name="noteTagDto">Модель привязки тэга к заметке.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>true - если произведено удаление привязки, false - если привязки не существует.</returns>
        Task<bool> RemoveTagFromNoteAsync(NoteTagDto noteTagDto, CancellationToken cancellationToken = default);
    }
}