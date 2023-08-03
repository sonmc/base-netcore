
using Base.Core;
using Microsoft.EntityFrameworkCore.Storage;

namespace Base.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IAuth Auth { get; }
        IUser User { get; }

        int SaveChanges();
        Task SaveChangesAsync();
        IExecutionStrategy CreateExecutionStrategy();
        IDbContextTransaction BeginTransaction();
    }

    public class ZUnitOfWork : IUnitOfWork
    {
        private readonly DataContext dbContext;

        public IAuth Auth { get; }
        public IUser User { get; }

        public ZUnitOfWork()
        {
            this.dbContext = new DataContext();
            this.Auth = new AuthService(this.dbContext);
            this.User = new UserService(this.dbContext);
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return this.dbContext.Database.CreateExecutionStrategy();
        }

        public IDbContextTransaction BeginTransaction()
        { 
            return this.dbContext.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return this.dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dbContext.Dispose();
            }
        }
    }
}
