namespace NotesApp.Domain.Entities
{
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Псевдоним пользователя.
        /// </summary>
        public string Login { get; set; } = null!;
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string PasswordHash { get; set; } = null!;
    }
}
