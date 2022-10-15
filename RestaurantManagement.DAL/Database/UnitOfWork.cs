using Microsoft.EntityFrameworkCore.Storage;
using RestaurantManagement.Core.Services.Contracts;

namespace RestaurantManagement.DAL.Database
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RestaurantManagementContext _dbContext;
        private IDbContextTransaction _transaction;
        private bool _disposedValue;
        public UnitOfWork(RestaurantManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
            DisposeTransaction();
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
            DisposeTransaction();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    DisposeTransaction();
                }

                _disposedValue = true;
            }
        }

        private void DisposeTransaction()
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }
}
