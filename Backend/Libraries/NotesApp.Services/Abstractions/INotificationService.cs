using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Services.Abstractions
{
    public interface INotificationService
    {
        /// <summary>
        /// Получение списка DTO напоминаний с применением запроса OData.
        /// </summary>
        /// <returns>Список DTO напоминаний.</returns>
        IQueryable<NotificationViewDto> GetQueryable();
        /// <summary>
        /// Создание новой заметки.
        /// </summary>
        /// <param name="notificationCreateDto">Модель напоминания с необходимыми для создания напоминания данными.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>Идентификатор созданного напоминания.</returns>
        Task<Guid> CreateNotificationAsync(NotificationCreateDto notificationCreateDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Изменение напоминания.
        /// </summary>
        /// <param name="notificationUpdateDto">Модель напоминания с данными, необходимыми для внесения изменений.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>true - если были внесены изменения, false -  если изменений не было.</returns>
        Task<bool> UpdateNotificationAsync(NotificationUpdateDto notificationUpdateDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удаление заметки.
        /// </summary>
        /// <param name="id">Идентификатор напоминания.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>true - если произведено удаление, false - если заметки не существует.</returns>
        Task<bool> DeleteNotificationAsync(Guid id, CancellationToken cancellationToken = default);
    }
}