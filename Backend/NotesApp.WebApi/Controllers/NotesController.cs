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

        [HttpGet]
        [EnableQuery]
        public IQueryable<NoteViewDto> Get()
        {
            return _service.GetQueryable();
        }
    }
}