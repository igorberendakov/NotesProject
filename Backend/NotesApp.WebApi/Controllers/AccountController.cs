using Microsoft.AspNetCore.Mvc;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Services.Abstractions;

namespace NotesApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService tagService)
        {
            _service = tagService;
        }
        /// <summary>
        /// Создание нового пользователя.
        /// </summary>
        /// <param name="userDto">Модель данных пользователя, содержащая необходимые для регистрации данные.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>Идентификатор созданного пользователя.</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserDto userDto, CancellationToken cancellationToken = default)
        {
            var result = await _service.RegisterUserAsync(userDto, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserDto userDto, CancellationToken cancellationToken = default)
        {
            var result = await _service.LoginUserAsync(userDto, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}