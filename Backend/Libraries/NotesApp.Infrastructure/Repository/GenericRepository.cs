using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotesApp.Domain.Abstractions;
using NotesApp.Infrastructure.Configurations.Authentication.Models;

namespace NotesApp.Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private protected readonly IDbContext _dbContext;
        private protected readonly DbSet<TEntity> _dbSet;
        private protected readonly ILogger<GenericRepository<TEntity>> _logger;
        private protected readonly Guid _currentUserId;

        public GenericRepository(IDbContext dbContext, ILogger<GenericRepository<TEntity>> logger, CurrentUser currentUser)
        {
            _currentUserId = currentUser.UserId!.Value;
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _logger = logger;
        }

        public virtual async Task<Guid> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.UserId = _currentUserId;
            await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Entity} с идентификатором {EntityId} успешно добавлен(а/о).", entity.GetType().Name, entity.Id);

            return entity.Id;
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Entity} с идентификатором {EntityId} успешно удален(а/о).", entity.GetType().Name, entity.Id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(x => x.UserId == _currentUserId).AsNoTracking().ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(x => x.UserId == _currentUserId).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _dbSet.Where(x => x.UserId == _currentUserId).AsNoTracking();
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
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