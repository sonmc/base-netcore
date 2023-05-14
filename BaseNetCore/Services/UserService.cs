
using BaseNetCore.Utils;
using BaseNetCore.Infrastructure.Helper;
using BaseNetCore.Infrastructure.Helper.Constant;
using BaseNetCore.Infrastructure.Repositories;
using BaseNetCore.Infrastructure.Schemas;
using System.Text;

namespace BaseNetCore.Services
{
    public interface IUserService
    {
        Response GetUser(string username);
        Response Get(int id);
        Response CheckPermission(User user, string actionName);
        Response Compare(string userPassword, string password);
        Response UpdateLoginTime(int userId);
        Response SetRefreshToken(string refreshToken, int userId);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IPermRepository permRepository;

        public UserService(IUserRepository _userRepository, IPermRepository _permRepository)
        {
            this.userRepository = _userRepository;
            this.permRepository = _permRepository;
        }

        public Response GetUser(string username)
        {
            User user = userRepository.GetByName(username);
            return new Response(user == null ? Message.ERROR : Message.SUCCESS, user);
        }

        public Response CheckPermission(User user, string actionName)
        {
            bool isAccess = false;
            //List<Permission> apis = permRepository.Get((int)user.RoleId);
            //foreach (var a in apis)
            //{
            //    if (a.Name.ToLower().IndexOf(actionName.ToLower()) > -1)
            //    {
            //        isAccess = true;
            //    }
            //}
            return new Response(Message.SUCCESS, isAccess);
        }

        public Response Get(int id)
        {
            User user = userRepository.Get(id);
            return new Response(user == null ? Message.ERROR : Message.SUCCESS, user);
        }

        public Response Compare(string userPassword, string password)
        {
            string hash = Jwt.MD5Hash(password);
            if (hash.ToLower().Equals(userPassword.ToLower()))
            {
                return new Response(Message.SUCCESS, hash);
            }
            return new Response(Message.ERROR, hash);
        }

        public Response UpdateLoginTime(int userId)
        {
            User user = userRepository.Get(userId);
            user.LastLogin = DateTime.UtcNow;
            return new Response(Message.SUCCESS, user);
        }

        public Response SetRefreshToken(string refreshToken, int userId)
        {
            User user = userRepository.Get(userId);
            user.HashRefreshToken = refreshToken;
            userRepository.Update(user);
            return new Response(Message.SUCCESS, user);
        }

    }
}
