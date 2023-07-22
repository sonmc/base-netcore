using Base.Core;
using Base.Core.Schemas;
using Base.Utils;


namespace Base.Services
{
    public interface IUser
    {
        Response Get(string username);
        Response Get(int id);
        Response List();
        Response Create(UserSchema user);
        Response Delete(int id);
        Response SetRefreshToken(string refreshToken, int userId);
        Response UpdateLoginTime(int userId);
    }

    public class UserService : IUser
    {

        private readonly DataContext context;
        public UserService(DataContext _ctx)
        {
            this.context = _ctx;
        }

        public Response Get(string username)
        {
            UserSchema user = context.Users.Where(x => x.UserName.Equals(username)).FirstOrDefault();
            return new Response(user == null ? Message.ERROR : Message.SUCCESS, user);
        }

        public Response List()
        {
            var users = context.Users.ToList();
            return new Response(Message.SUCCESS, users);
        }

        public Response Create(UserSchema u)
        {
            var user = context.Users.Add(u);
            context.SaveChanges();
            return new Response(Message.SUCCESS, user);
        }

        public Response Delete(int id)
        {
            var user = context.Users.Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
            return new Response(Message.SUCCESS, id);
        }

        public Response Get(int id)
        {
            UserSchema user = context.Users.Find(id);
            return new Response(user == null ? Message.ERROR : Message.SUCCESS, user);
        }

        public Response UpdateLoginTime(int userId)
        {
            UserSchema user = context.Users.Find(userId);
            user.LastLogin = DateTime.UtcNow;
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
