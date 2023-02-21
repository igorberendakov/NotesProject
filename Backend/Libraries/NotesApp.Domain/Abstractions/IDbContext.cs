using Microsoft.EntityFrameworkCore;

namespace NotesApp.Domain.Abstractions
{
    /// <summary>
    /// Абстракция контекста базы данных.
    /// </summary>
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
