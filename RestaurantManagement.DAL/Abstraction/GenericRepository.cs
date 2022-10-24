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
            _dbSet = dbContext.Set<T>();
            _dbContext = dbContext;
            _authService = authService;
        }

        public virtual async Task<IEnumerable<T>> BulkInsert(IEnumerable<T> entities,
            CancellationToken cancellationToken = default)
        {
            var list = new List<T>();
            foreach (var entity in entities)
            {
                list.Add(await InsertAsync(entity, cancellationToken));
            }
            return list;
        }

        public virtual bool BulkRemove(IEnumerable<T> entities, CancellationToken cancellationToken = default)
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

        public virtual IEnumerable<T> BulkUpdate(IEnumerable<T> entities,
            CancellationToken cancellationToken = default)
        {
            return entities.Select(entity => Update(entity, cancellationToken)).ToList();
        }

        public virtual async ValueTask<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            return await query!.Where(predicate).CountAsync(cancellationToken);
        }

        public virtual async ValueTask<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            return await query!.AnyAsync(predicate, cancellationToken);
        }
        public virtual IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _dbSet;

            return query!.AsNoTracking();
        }

        public virtual async Task<IEnumerable<T>> GetAsync<TKey>(Expression<Func<T, bool>> predicate,
                                                           CancellationToken cancellationToken,
                                                           int? count = null,
                                                           int? currentPage = null,
                                                           Expression<Func<T, TKey>> orderBy = null,
                                                           SortDirection? sortDirection = null,
                                                           Expression<Func<T, object>>[] includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate is not null)
                query = query!.Where(predicate);

            if (orderBy is not null)
            {
                if (sortDirection is not null && sortDirection == SortDirection.Asc)
                    query = query!.OrderBy(orderBy);
                if (sortDirection is not null && sortDirection == SortDirection.Desc)
                    query = query!.OrderByDescending(orderBy);
            }

            if (count is not null)
            {
                query = currentPage is not null
                    ? query!.Skip(((int)currentPage) * (int)count).Take((int)count)
                    : query!.Take((int)count);
            }

            if (includes is null) return await query!.ToListAsync(cancellationToken);

            query = includes.Aggregate(query, (current, include) => current!.Include(include));

            return await query!.ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var data = await GetAsync<T>(x => x.Id == id, CancellationToken.None);
            return data.SingleOrDefault();
        }

        public virtual async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity is IDateMetadata dateMetadata)
                {
                    dateMetadata.UpdateDate = _authService.GetSystemDate();
                    dateMetadata.CreateDate = _authService.GetSystemDate();
                }

                if (entity is IAuditMetadata auditMetadata)
                {
                    auditMetadata.UpdatedByUserId = _authService.GetUserId();
                    auditMetadata.CreatedByUserId = _authService.GetUserId();
                }

                //entity.RowVersion = Guid.NewGuid();

                _dbSet.Add(entity);

                await _dbContext.SaveChangesAsync(cancellationToken);
                return entity;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            if (_dbSet != null)
            {
                var entity = await _dbSet.FindAsync(new object[] { id, cancellationToken }, cancellationToken: cancellationToken);

                if (entity != null)
                    Remove(entity, cancellationToken);
            }
        }

        public virtual void Remove(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public virtual T Update(T entityToUpdate, CancellationToken cancellationToken = default)
        {
            if (entityToUpdate is IDateMetadata dateMetadata)
                dateMetadata.UpdateDate = _authService.GetSystemDate();

            if (entityToUpdate is IAuditMetadata auditMetadata)
                auditMetadata.UpdatedByUserId = _authService.GetUserId();

            //entityToUpdate.RowVersion = Guid.NewGuid();
            _dbContext.Update(entityToUpdate);

            return entityToUpdate;
        }
    }
}
