using System.Linq;
using System.Linq.Expressions;
using RestaurantManagement.Core;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Metadata;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Abstraction;

namespace RestaurantManagement.DAL.Abstraction
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly IAuthService _authService;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task BulkInsert(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
                await InsertAsync(entity, cancellationToken);
        }

        public virtual bool BulkRemove(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var entity in entities)
                {
                    Remove(entity, cancellationToken);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async ValueTask BulkUpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
            {
               await  UpdateAsync(entity, cancellationToken);
            }
        }

        public virtual async ValueTask<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predicate).CountAsync(cancellationToken);
        }

        public virtual async ValueTask<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual async Task<IEnumerable<T>> GetAsync<TKey>(Expression<Func<T, bool>> predicate,
                                                           CancellationToken cancellationToken,
                                                           int? count = null,
                                                           int? currentPage = null,
                                                           Expression<Func<T, TKey>>? orderBy = null,
                                                           SortDirection? sortDirection = null,
                                                           Expression<Func<T, object>>[]? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate is not null)
                query.Where(predicate);

            if (orderBy is not null)
            {
                if (sortDirection is not null && sortDirection == SortDirection.Asc)
                    query = query.OrderBy(orderBy);
                if (sortDirection is not null && sortDirection == SortDirection.Desc)
                    query = query.OrderByDescending(orderBy);
            }

            if (count is not null)
            {
                if (currentPage is not null)
                    query.Skip(((int)currentPage) * (int)count).Take((int)count);
                else query.Take((int)count);
            }

            if (includes is not null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

            return await query.ToListAsync(cancellationToken);
        }

        public virtual async Task<T> InsertAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity is IRestaurant)
                if (((IRestaurant)entity).RestaurantId != ((IRestaurant)entity).RestaurantId
                    && _authService.GetRoleName() != "SuperAdmin")
                    throw new InvalidOperationException("insufficient privileges to update entity");

            if (entity is null)
            {
                throw new Exception();
            }

            if (entity is IDateMetadata)
            {
                ((IDateMetadata)entity).UpdateDate = _authService.GetSystemDate();
                ((IDateMetadata)entity).CreateDate = _authService.GetSystemDate();
            }

            if (entity is IAuditMetadata)
            {
                ((IAuditMetadata)entity).UpdatedByUserId = _authService.GetUserId();
                ((IAuditMetadata)entity).CreatedByUserId = _authService.GetUserId();
            }

            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public virtual async Task RemoveAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is IRestaurant)
                if (((IRestaurant)entity).RestaurantId != ((IRestaurant)entity).RestaurantId
                    && _authService.GetRoleName() != "SuperAdmin")
                    throw new InvalidOperationException("insufficient privileges to update entity");

            if (entity != null)
                Remove(entity, cancellationToken);
        }

        public virtual void Remove(T entity, CancellationToken cancellationToken)
        {
            if (entity is IRestaurant)
                if (((IRestaurant)entity).RestaurantId != ((IRestaurant)entity).RestaurantId
                    && _authService.GetRoleName() != "SuperAdmin")
                    throw new InvalidOperationException("insufficient privileges to update entity");

            _dbContext.Entry<T>(entity).State = EntityState.Deleted;
        }

        public virtual Task<T> UpdateAsync(T entityToUpdate, CancellationToken cancellationToken)
        {
            if (entityToUpdate is IRestaurant)
                if (((IRestaurant)entityToUpdate).RestaurantId != ((IRestaurant)entityToUpdate).RestaurantId
                    && _authService.GetRoleName() != "SuperAdmin")
                    throw new InvalidOperationException("insufficient privileges to update entity");

            if (entityToUpdate is null)
            {
                throw new Exception();
            }

            if (entityToUpdate is IDateMetadata)
                ((IDateMetadata)entityToUpdate).UpdateDate = _authService.GetSystemDate();

            if (entityToUpdate is IAuditMetadata)
                ((IAuditMetadata)entityToUpdate).UpdatedByUserId = _authService.GetUserId();

            _dbContext.Update(entityToUpdate);

            return Task.FromResult(entityToUpdate);
        }
    }
}
