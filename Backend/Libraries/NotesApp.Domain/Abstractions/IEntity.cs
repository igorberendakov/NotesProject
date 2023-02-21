namespace NotesApp.Domain.Abstractions
{
    /// <summary>
    /// Абстракция сущности.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        Guid Id { get; set; }
    }
}
