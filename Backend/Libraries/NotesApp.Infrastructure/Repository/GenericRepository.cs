using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotesApp.Domain.Abstractions;

namespace NotesApp.Infrastructure.Repository
{
    internal class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogger<GenericRepository<TEntity>> _logger;

        public GenericRepository(IDbContext dbContext, ILogger<GenericRepository<TEntity>> logger)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Entity} с идентификатором {EntityId} успешно добавлен(а/о).", entity.GetType().Name, entity.Id);

            return entity.Id;
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Entity} с идентификатором {EntityId} успешно удален(а/о).", entity.GetType().Name, entity.Id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbSet;
        }

        public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                _logger.LogWarning("Попытка изменить {Entity} с идентификатором {EntityId} не привела к изменениям данных.", entity.GetType().Name.ToLower(), entity.Id);
            }
            else
            {
                _logger.LogInformation("{Entity} с идентификатором {EntityId} успешно изменен(а/о).", entity.GetType().Name, entity.Id);
            }

            return result;
        }
    }
}
