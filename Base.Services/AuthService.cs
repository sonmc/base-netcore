using Base.Core;
using Base.Core.Schemas;
using Base.Utils;

namespace Base.Services
{
    public interface IAuth
    {
        void UpdateLoginTime(int userId);
        UserSchema SetRefreshToken(string refreshToken, int userId);
    }

    public class AuthService : IAuth
    {
        private readonly DataContext context;
        public AuthService(DataContext ctx)
        {
            this.context = ctx;
        }

        public void UpdateLoginTime(int userId)
        {
            UserSchema user = context.Users.Find(userId);
            user.LastLogin = DateTime.UtcNow;
            context.Users.Update(user); 
        }

        public UserSchema SetRefreshToken(string refreshToken, int userId)
        {
            UserSchema user = context.Users.Find(userId);
            user.HashRefreshToken = refreshToken;
            context.Users.Update(user);
            return user;
        }

    }
}
