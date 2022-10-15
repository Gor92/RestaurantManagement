using System.Linq.Expressions;
using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.Core.Repositories.Abstraction
{
    /// <summary>
    /// This is the repository interface for any implementation of
    /// <typeparamref name="T"/>, exposing asynchronous C.R.U.D. functionality
    /// </summary>
    /// <typeparam name="T">The <see cref="BaseEntity"/> implementation class type</typeparam>
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Creates an item representing the given <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">The item values to create</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A newly created item instance Id</returns>
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates multiple items representing the given <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">The items to be created</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<IEnumerable<T>> BulkInsert(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the object that corresponds to the given <paramref name="entityToUpdate"/>
        /// </summary>
        /// <param name="entityToUpdate">The item value to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A <see cref="Task{T}"/> representing the <see cref="BaseEntity"/> implementation class instance as a <typeparamref name="T"/></returns>
        T Update(T entityToUpdate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates multiple object that corresponds to the given <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">The items to be updated</param>
        /// <param name="cancellationToken">The cancellation token</param>
        IEnumerable<T> BulkUpdate(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> collection of <see cref="BaseEntity"/>
        /// implementation classes that match the given <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">The expression used for evaluating a matching item</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <param name="count">entity count</param>
        /// <param name="currentPage">Current Page</param>
        /// <param name="orderBy">The expression used for evaluating order by functionality</param>
        /// <param name="sortDirection"> Sort Direction (ASC or DESC)</param>
        /// <param name="includes"></param>
        /// <returns>A collection of item instances who meet the <paramref name="predicate"/> condition</returns>
        Task<IEnumerable<T>> GetAsync<TKey>(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken,
            int? count = null,
            int? currentPage = null,
            Expression<Func<T, TKey>>? orderBy = null,
            SortDirection? sortDirection = null,
            Expression<Func<T, object>>[]? includes = null);

        /// <summary>
        /// Gets the count of <see cref="T"/> for given predicate
        /// </summary>
        /// <param name="predicate">The expression used for evaluating any matching items</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The count of entities</returns>
        ValueTask<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the object that corresponds to the given <paramref name="id"/>
        /// </summary>
        /// <param name="id">The string identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task RemoveAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the given <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">The item to be deleted</param>
        /// <param name="cancellationToken">The Cancellation token</param>
        /// <returns>Boolean value if all cart items deleted successfully or not </returns>
        void Remove(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the given <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">The items to be deleted</param>
        /// <param name="cancellationToken">The Cancellation token</param>
        /// <returns>Boolean value if all cart items deleted successfully or not </returns>
        bool BulkRemove(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Queries to see if an item exists.
        /// </summary>
        /// <param name="predicate">The expression used for evaluating any matching items</param>
        /// <param name="cancellationToken">The Cancellation token</param>
        /// <returns>Boolean value if all cart items deleted successfully or not</returns>
        ValueTask<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        IEnumerable<T> GetAll();
    }
}
