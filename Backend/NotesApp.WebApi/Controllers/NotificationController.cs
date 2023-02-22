using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Services.Abstractions;

namespace NotesApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService noteService)
        {
            _service = noteService;
        }
        /// <summary>
        /// Получение списка заметок, поддерживающего синтаксис запросов Odata.
        /// </summary>
        /// <returns>Список заметок, поддерживающий запросы Odata.</returns>
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
        /// <returns>Идентификатор созданного напоминания.</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> PostAsync([FromBody] NotificationCreateDto notificationCreateDto, CancellationToken cancellationToken = default)
        {
            return Ok(await _service.CreateNotificationAsync(notificationCreateDto, cancellationToken));
        }
    }
}