using Base.Core;
using Base.Core.Schemas; 


namespace Base.Services
{
    public interface IUser : IBaseService<UserSchema>
    {
        UserSchema SetRefreshToken(string refreshToken, int userId);
        UserSchema UpdateLoginTime(int userId);
        List<UserSchema> Get(string name);
        bool CheckPermissionAction(int user, string action);
    }

    public class UserService : ZBaseService<UserSchema, DataContext>, IUser
    {

        private readonly DataContext context;
        public UserService(DataContext _ctx) : base(_ctx)
        {
            this.context = _ctx;
        }

        public UserSchema UpdateLoginTime(int userId)
        {
            UserSchema user = context.Users.Find(userId);
            user.LastLogin = DateTime.UtcNow;
            return user;
        }

        public UserSchema SetRefreshToken(string refreshToken, int userId)
        {
            UserSchema user = context.Users.Find(userId);
            user.HashRefreshToken = refreshToken;
            context.Users.Update(user);
            return user;
        }

        public List<UserSchema> Get(string name)
        {
            List<UserSchema> users = context.Users.Where(u=>u.UserName.Equals(name)).ToList();
            return users;
        }

        public bool CheckPermissionAction(int userId, string action)
        {
            throw new NotImplementedException();
        }
    }
}
