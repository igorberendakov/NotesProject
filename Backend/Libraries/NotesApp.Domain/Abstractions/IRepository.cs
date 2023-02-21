namespace NotesApp.Domain.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Получение queryableсписка сущностей.
        /// </summary>
        /// <returns>Queryable список сущностей.</returns>
        IQueryable<TEntity> GetQueryable();
        /// <summary>
        /// Получение списка всех сущностей.
        /// </summary>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>Список всех сущностей.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Получение сущности по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>Сущность с указанным идентификатором.</returns>
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Добавление сущности.
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>Идентификатор добавленной сущности.</returns>
        Task<Guid> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Изменение сущности.
        /// </summary>
        /// <param name="entity">Сущность с измененными свойствами.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        /// <returns>true - если были внесены изменения, false - если изменений не было.</returns>
        Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удаление сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен прерывания операции.</param>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}