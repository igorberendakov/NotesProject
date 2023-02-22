using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Services.Abstractions;

namespace NotesApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _service;

        public NotesController(INoteService noteService)
        {
            _service = noteService;
        }
        /// <summary>
        /// Получение списка заметок, поддерживающего синтаксис запросов Odata.
        /// </summary>
        /// <returns>Список заметок, поддерживающий запросы Odata.</returns>
        /// <response code="200">Список заметок.</response>
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<NoteViewDto>> GetAsync()
        {
            return Ok(_service.GetQueryable());
        }
        /// <summary>
        /// Создание новой заметки.
        /// </summary>
        /// <param name="noteCreateDto">Модель заметки, содержащая необходимые для создания заметки данные.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <response code="200">Идентификатор созданной заметки.</response>
        [HttpPost]
        public async Task<ActionResult<Guid>> PostAsync([FromBody] NoteCreateDto noteCreateDto, CancellationToken cancellationToken = default)
        {
            return Ok(await _service.CreateNoteAsync(noteCreateDto, cancellationToken));
        }
        /// <summary>
        /// Изменение данных заметки.
        /// </summary>
        /// <param name="noteUpdateDto">Модель заметки, содержащая необходимые для изменения заметки данные.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <response code="200">Заметка успешно изменена.</response>
        /// <response code="404">Заметка с данным идентификатором не найдена.</response>
        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] NoteUpdateDto noteUpdateDto, CancellationToken cancellationToken = default)
        {
            var result = await _service.UpdateNoteAsync(noteUpdateDto, cancellationToken);

            if (result)
            {
                return Ok();
            }

            return NotFound("Заметка с таким id не найдена.");
        }
        /// <summary>
        /// Удаление заметки.
        /// </summary>
        /// <param name="id">Идентификатор заметки.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <response code="200">Заметка успешно удалена.</response>
        /// <response code="204">Заметка не существует.</response>
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _service.DeleteNoteAsync(id, cancellationToken);

            if (result)
            {
                return Ok();
            }

            return NoContent();
        }
    }
}