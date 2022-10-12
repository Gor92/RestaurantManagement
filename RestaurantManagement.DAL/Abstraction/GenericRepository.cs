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
        private protected DbSet<T> DbSet;

        public GenericRepository(DbContext dbContext, IAuthService authService)
        {
            DbSet = dbContext.Set<T>();
            _dbContext = dbContext;
            _authService = authService;
        }

        public virtual  IEnumerable<T> BulkInsert(IEnumerable<T> entities,
            CancellationToken cancellationToken = default)
        {
            var list = new List<T>();
            foreach (var entity in entities)
                 list.Add(Insert(entity, cancellationToken));

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
            var list = new List<T>();
            foreach (var entity in entities)
            {
                list.Add(Update(entity, cancellationToken));
            }

            return list;
        }

        public virtual async ValueTask<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(predicate).CountAsync(cancellationToken);
        }

        public virtual async ValueTask<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(predicate, cancellationToken);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public virtual async Task<IEnumerable<T>> GetAsync<TKey>(Expression<Func<T, bool>> predicate,
                                                           CancellationToken cancellationToken,
                                                           int? count = null,
                                                           int? currentPage = null,
                                                           Expression<Func<T, TKey>>? orderBy = null,
                                                           SortDirection? sortDirection = null,
                                                           Expression<Func<T, object>>[]? includes = null)
        {
            IQueryable<T> query = DbSet;

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

        public async Task<T> GetByIdAsync(int id)
        {
            var data =await GetAsync<T>(x => x.Id == id,CancellationToken.None);
            return data.SingleOrDefault();
        }

        public virtual T Insert(T entity, CancellationToken cancellationToken = default)
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

            DbSet.Add(entity);
            return entity;
        }

        public virtual async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity is IRestaurant)
                if (((IRestaurant)entity).RestaurantId != ((IRestaurant)entity).RestaurantId
                    && _authService.GetRoleName() != "SuperAdmin")
                    throw new InvalidOperationException("insufficient privileges to update entity");

            if (entity != null)
                Remove(entity, cancellationToken);
        }

        public virtual void Remove(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is IRestaurant)
                if (((IRestaurant)entity).RestaurantId != ((IRestaurant)entity).RestaurantId
                    && _authService.GetRoleName() != "SuperAdmin")
                    throw new InvalidOperationException("insufficient privileges to update entity");

            _dbContext.Entry<T>(entity).State = EntityState.Deleted;
        }

        public virtual T Update(T entityToUpdate, CancellationToken cancellationToken = default)
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

            return entityToUpdate;
        }
    }
}
