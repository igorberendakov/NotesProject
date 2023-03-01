using NotesApp.Infrastructure.Configurations.Authentication.Models;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.Services.Abstractions
{
    public interface IAccountService
    {
        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <param name="userDto">Модель данных пользователя.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        Task<LoginResult> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="userDto">Модель данных для входа пользователя.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        Task<LoginResult> LoginUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
    }
}
