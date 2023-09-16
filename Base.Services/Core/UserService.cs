using Base.Core;
using Base.Core.Schemas;

namespace Base.Services
{
    public interface IUser : IBaseService<User>
    {
        User SetRefreshToken(string refreshToken, int userId);
        User UpdateLoginTime(int userId);
        List<User> Get(string name);
        bool CheckPermission(int user, string module, string action);
    }


    public class UserService : BaseService<User, DataContext>, IUser
    {

        private readonly DataContext context;

        public UserService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }

        public User UpdateLoginTime(int userId)
        {
            User user = context.Users.Find(userId);
            user.LastLogin = DateTime.UtcNow;
            return user;
        }


        public User SetRefreshToken(string refreshToken, int userId)
        {
            User user = context.Users.Find(userId);
            user.HashRefreshToken = refreshToken;
            return user;
        }

        public List<User> Get(string name)
        {
            List<User> users = context.Users.Where(u => u.UserName.Equals(name)).ToList();
            return users;
        }

        public bool CheckPermission(int userId, string module, string action)
        {
            Perm perm = (from p in context.Perms
                               join gp in context.GroupsPerms on p.Id equals gp.PermId
                               join g in context.Groups on gp.GroupId equals g.Id
                               join ug in context.UsersGroups on g.Id equals ug.GroupId
                               where ug.UserId == userId && p.Action == action && p.Module == module
                               select p).FirstOrDefault();

            return perm != null;
        }


    }
}
