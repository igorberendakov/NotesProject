﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Services.Abstractions;

namespace NotesApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;

        public TagsController(ITagService tagService)
        {
            _service = tagService;
        }
        /// <summary>
        /// Получение списка тэгов, поддерживающего синтаксис запросов Odata.
        /// </summary>
        /// <returns>Список тэгов, поддерживающий запросы Odata.</returns>
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<TagViewDto>> Get()
        {
            return Ok(_service.GetQueryable());
        }
        /// <summary>
        /// Создание нового тэга.
        /// </summary>
        /// <param name="tagCreateDto">Модель тэга, содержащая необходимые для создания тэга данные.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>Идентификатор созданного тэга.</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> PostAsync([FromBody] TagCreateDto tagCreateDto, CancellationToken cancellationToken = default)
        {
            return Ok(await _service.CreateTagAsync(tagCreateDto, cancellationToken));
        }
        /// <summary>
        /// Добавление привязки тэга к заметке.
        /// </summary>
        /// <param name="noteTagDto">Идентификаторы заметки и тэга.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <response code="200">Тэг успешно привязан к заметке.</response>
        [HttpPost("NoteTag")]
        public async Task<ActionResult> PostNoteTagAsync([FromBody] NoteTagDto noteTagDto, CancellationToken cancellationToken = default)
        {
            await _service.AddTagToNoteAsync(noteTagDto, cancellationToken);

            return Ok();
        }
        /// <summary>
        /// Изменение данных тэга.
        /// </summary>
        /// <param name="tagUpdateDto">Модель тэга, содержащая необходимые для изменения тэга данные.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <response code="200">Тэг успешно изменен.</response>
        /// <response code="404">Тэг с данным идентификатором не найден.</response>
        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] TagUpdateDto tagUpdateDto, CancellationToken cancellationToken = default)
        {
            var result = await _service.UpdateTagAsync(tagUpdateDto, cancellationToken);

            if (result)
            {
                return Ok();
            }

            return NotFound("Тэг с таким id не найден.");
        }
        /// <summary>
        /// Удаление тэга.
        /// </summary>
        /// <param name="id">Идентификатор тэга.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <response code="200">Тэг успешно удалена.</response>
        /// <response code="204">Тэг не существует.</response>
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _service.DeleteTagAsync(id, cancellationToken);

            if (result)
            {
                return Ok();
            }

            return NoContent();
        }
        /// <summary>
        /// Удаление тэга.
        /// </summary>
        /// <param name="noteTagDto">Идентификаторы заметки и тэга.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <response code="200">Тэг успешно откреплен от заметки.</response>
        /// <response code="204">Тэг не привязан к заметке.</response>
        [HttpDelete("NoteTag")]
        public async Task<ActionResult> DeleteNoteTagAsync([FromBody] NoteTagDto noteTagDto, CancellationToken cancellationToken = default)
        {
            var result = await _service.RemoveTagFromNoteAsync(noteTagDto, cancellationToken);

            if (result)
            {
                return Ok();
            }

            return NoContent();
        }
    }
}