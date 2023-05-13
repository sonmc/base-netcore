using base_netcore.Infrastructure.Helper;
using base_netcore.Services;

namespace base_netcore.UseCase.Auth
{
    public interface IAuthFlow
    {
        Response Login(string username, string password);
    }

    public class AuthFlow : IAuthFlow
    {
        readonly IUserService userService;
        public AuthFlow(IUserService _service)
        {
            userService = _service;
        }

        public Response Login(string username, string password)
        {
            Response response = userService.GetUser(username);
            return response;
        }
    }
}
