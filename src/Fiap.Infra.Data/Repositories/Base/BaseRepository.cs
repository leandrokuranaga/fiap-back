using Fiap.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fiap.Infra.Data.Repositories.Base
{
    public abstract class BaseRepository<TEntity>(IUnitOfWork unitOfWork) : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            var dbSet = _unitOfWork.Context.Set<TEntity>();
            var entry = _unitOfWork.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                var exists = await dbSet.FindAsync(entry.Property("Id").CurrentValue);
                if (exists != null)                
                    dbSet.Update(entity);
                else
                    await dbSet.AddAsync(entity);
            }

            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entity)
        {
            await _unitOfWork.Context.Set<TEntity>().AddRangeAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _unitOfWork.Context.Set<TEntity>().Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<TEntity> GetAll() => _unitOfWork.Context.Set<TEntity>().AsQueryable();

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression) => await GetAll().Where(expression).ToListAsync();

        public async Task<TEntity> GetByIdAsync(int id, bool noTracking) => noTracking ? await _unitOfWork.Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id) : await _unitOfWork.Context.Set<TEntity>().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression) => GetAll().Where(expression);

        public virtual IQueryable<TEntity> Include<TProperty>(IQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> path) => query.Include(path);

        public async Task DeleteAsync(TEntity entity)
        {
            _unitOfWork.Context.Set<TEntity>().Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await GetAll().ToListAsync();

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression) => await _unitOfWork.Context.Set<TEntity>().AsNoTracking().AnyAsync(expression);

        public async Task<TEntity> GetOneNoTracking(Expression<Func<TEntity, bool>> expression) => await _unitOfWork.Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(expression);

        public async Task<TEntity> GetOneTracking(Expression<Func<TEntity, bool>> expression) => await _unitOfWork.Context.Set<TEntity>().FirstOrDefaultAsync(expression);
        public async Task<IEnumerable<TEntity>> GetNoTrackingAsync(Expression<Func<TEntity, bool>> expression) => await GetAll().AsNoTracking().Where(expression).ToListAsync();

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _unitOfWork.Context.Set<TEntity>().AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _unitOfWork.Context.Set<TEntity>().UpdateRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateRangeNoTrackingAsync(IEnumerable<TEntity> entities)
        {
            _unitOfWork.Context.AttachRange(entities);
            _unitOfWork.Context.Set<TEntity>().UpdateRange(entities);
            await _unitOfWork.CommitAsync();
        }
    }
}
