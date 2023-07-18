using Base.Core;
using Base.Core.Schemas;
using Base.Utils; 

namespace BaseNetCore.Src.Services
{
    public interface IAuth
    {
        Response UpdateLoginTime(int userId);
        Response SetRefreshToken(string refreshToken, int userId);
    }

    public class AuthService : IAuth
    {
        private readonly DataContext context;
        public AuthService()
        {
            this.context = new DataContext();
        }
        public Response UpdateLoginTime(int userId)
        {
            UserSchema user = context.Users.Find(userId);
            user.LastLogin = DateTime.UtcNow;
            context.Users.Update(user);
            return new Response(Message.SUCCESS, user);
        }

        public Response SetRefreshToken(string refreshToken, int userId)
        {
            UserSchema user = context.Users.Find(userId);
            user.HashRefreshToken = refreshToken;
            context.Users.Update(user);
            return new Response(Message.SUCCESS, user);
        }

    }
}
