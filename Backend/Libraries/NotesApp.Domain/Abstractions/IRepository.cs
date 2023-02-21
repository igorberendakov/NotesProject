namespace NotesApp.Domain.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetQueryable();
        IEnumerable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}