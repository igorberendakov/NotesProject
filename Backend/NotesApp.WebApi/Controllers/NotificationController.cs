using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Services.Abstractions;

namespace NotesApp.WebApi.Controllers
{
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _service;
        
        public NotificationsController(INotificationService noteService)
        {
            _service = noteService;
        }
        /// <summary>
        /// Получение списка напоминаний, поддерживающего синтаксис запросов Odata.
        /// </summary>
        /// <returns>Список напоминаний, поддерживающий запросы Odata.</returns>
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<NotificationViewDto>> GetAsync()
        {
            return Ok(_service.GetQueryable());
        }
        /// <summary>
        /// Создание нового напоминания.
        /// </summary>
        /// <param name="notificationCreateDto">Модель напоминания, содержащая необходимые для создания напоминания данные.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>Идентификатор созданного напоминания.</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> PostAsync([FromBody] NotificationCreateDto notificationCreateDto, CancellationToken cancellationToken = default)
        {
            return Ok(await _service.CreateNotificationAsync(notificationCreateDto, cancellationToken));
        }
        /// <summary>
        /// Изменение данных напоминания.
        /// </summary>
        /// <param name="notificationUpdateDto">Модель напоминания, содержащая необходимые для изменения данных напоминания данные.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <response code="200">Напоминание успешно изменено.</response>
        /// <response code="404">Напоминание с данным идентификатором не найдено.</response>
        [Route("Put")]
        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] NotificationUpdateDto notificationUpdateDto, CancellationToken cancellationToken = default)
        {
            var result = await _service.UpdateNotificationAsync(notificationUpdateDto, cancellationToken);

            if (result)
            {
                return Ok();
            }

            return NotFound("Напоминание с таким id не найдено.");
        }
        /// <summary>
        /// Удаление напоминания.
        /// </summary>
        /// <param name="id">Идентификатор напоминания.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <response code="200">Напоминание успешно удалено.</response>
        /// <response code="204">Напоминание не существует.</response>
        [Route("Delete")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _service.DeleteNotificationAsync(id, cancellationToken);

            if (result)
            {
                return Ok();
            }

            return NoContent();
        }
    }
}