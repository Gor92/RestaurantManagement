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

        public virtual async void BulkInsert(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _dbContext.AddRangeAsync(entities, cancellationToken);
        }

        public virtual bool BulkRemove(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            try
            {
                _dbContext.RemoveRange(entities, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual void BulkUpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            _dbContext.UpdateRange(entities);
        }

        public virtual Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, int? count = null, int? currentPage = null, string? orderBy = null, SortDirection? sortDirection = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> InsertAsync(T entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual Task RemoveAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual Task RemoveAsync(T entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
