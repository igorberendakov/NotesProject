using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Services.Abstractions;

namespace NotesApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public ActionResult<IQueryable<TagViewDto>> GetAsync()
        {
            return Ok(_service.GetQueryable());
        }
        /// <summary>
        /// Создание нового тэга.
        /// </summary>
        /// <param name="tagCreateDto">Модель тэга, содержащая необходимые для создания тэга данные.</param>
        /// <returns>Идентификатор созданного тэга.</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> PostAsync([FromBody] TagCreateDto tagCreateDto, CancellationToken cancellationToken = default)
        {
            return Ok(await _service.CreateTagAsync(tagCreateDto, cancellationToken));
        }
    }
}