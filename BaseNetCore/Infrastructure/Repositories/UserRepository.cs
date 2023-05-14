using BaseNetCore.Infrastructure.Schemas;

namespace BaseNetCore.Infrastructure.Repositories
{
  
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByName(string name); 
    }

    public class UserRepository : BaseRepository<User, DataContext>, IUserRepository
    {
        DataContext dbContext;

        public UserRepository() { }

        public UserRepository(DataContext context) : base(context)
        {
            this.dbContext = context;
        }

        public User GetByName(string name)
        {
            return dbContext.Users.FirstOrDefault(x => x.UserName.Equals(name));
        }
    }
}
