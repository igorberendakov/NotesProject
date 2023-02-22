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
        /// <returns>Идентификатор созданной заметки.</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> PostAsync([FromBody] NoteCreateDto noteCreateDto, CancellationToken cancellationToken = default)
        {
            return Ok(await _service.CreateNoteAsync(noteCreateDto, cancellationToken));
        }
    }
}