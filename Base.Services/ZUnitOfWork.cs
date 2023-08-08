
using Base.Core;
using Microsoft.EntityFrameworkCore.Storage;

namespace Base.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IAuth Auth { get; }
        IUser Users { get; }
        IPerm Perms { get; }
        IGroup Groups { get; }
        IGroupPerm GroupsPerms {get;}
        IUserGroup UsersGroups { get; }

        int SaveChanges();
        Task SaveChangesAsync();
        IExecutionStrategy CreateExecutionStrategy();
        IDbContextTransaction BeginTransaction();
    }

    public class ZUnitOfWork : IUnitOfWork
    {
        private readonly DataContext dbContext;

        public IAuth Auth { get; }
        public IUser Users { get; }
        public IPerm Perms { get; }
        public IGroup Groups { get; }
        public IGroupPerm GroupsPerms { get; }
        public IUserGroup UsersGroups { get; }
         

        public ZUnitOfWork()
        {
            this.dbContext = new DataContext();
            this.Auth = new AuthService(this.dbContext);
            this.Users = new UserService(this.dbContext);
            this.Perms = new PermService(this.dbContext);
            this.Groups = new GroupService(this.dbContext);
            this.GroupsPerms = new GroupPermService(this.dbContext);
            this.UsersGroups = new UserGroupService(this.dbContext);
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
