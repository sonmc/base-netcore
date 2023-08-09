using Base.Core;
using Microsoft.EntityFrameworkCore.Storage;

namespace Base.Services.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IAuth Auth { get; }
        IUser Users { get; }
        IPerm Perms { get; }
        IGroup Groups { get; }
        IGroupPerm GroupsPerms { get; }
        IUserGroup UsersGroups { get; }

        int SaveChanges();
        Task SaveChangesAsync();
        IExecutionStrategy CreateExecutionStrategy();
        IDbContextTransaction BeginTransaction();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dbContext;

        public IAuth Auth { get; }
        public IUser Users { get; }
        public IPerm Perms { get; }
        public IGroup Groups { get; }
        public IGroupPerm GroupsPerms { get; }
        public IUserGroup UsersGroups { get; }


        public UnitOfWork()
        {
            dbContext = new DataContext();
            Auth = new AuthService(dbContext);
            Users = new UserService(dbContext);
            Perms = new PermService(dbContext);
            Groups = new GroupService(dbContext);
            GroupsPerms = new GroupPermService(dbContext);
            UsersGroups = new UserGroupService(dbContext);
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return dbContext.Database.CreateExecutionStrategy();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return dbContext.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
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
                dbContext.Dispose();
            }
        }
    }
}
