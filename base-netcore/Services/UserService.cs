using base_netcore.Infrastructure.Helper;
using base_netcore.Infrastructure.Helper.Constant;
using base_netcore.Infrastructure.Repositories;
using base_netcore.Infrastructure.Schemas;

namespace base_netcore.Services
{
    public interface IUserService
    {
        Response GetUser(string username);
        Response Get(int id);
        Response CheckPermission(User user, string actionName);
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
            return new Response(Message.SUCCESS, user);
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
            return new Response(Message.SUCCESS, user); 
        }
    }
}
