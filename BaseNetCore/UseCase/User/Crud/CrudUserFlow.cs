using Base.Core.Schemas;
using Base.Utils;  
using BaseNetCore.Src.Services;  

namespace BaseNetCore.Src.UseCase.User
{

    public class CrudUserFlow
    {
        readonly IUser userService;
        public CrudUserFlow(IUser _service)
        {
            userService = _service;
        }

        public Response GetCurrentUser(string token)
        {
            string userCredentialString = JwtUtil.GetIdByToken(token);
            int id = 0;
            if (!Int32.TryParse(userCredentialString, out id))
            {
                return new Response(Message.ERROR, null);
            }
            Response response = userService.Get(id);

            if (response.Status == Message.ERROR)
            {
                return new Response(Message.ERROR, null);
            }
            UserSchema user = (UserSchema)response.Result;
            return new Response(Message.SUCCESS, user);
        }

        public Response List()
        {
            Response response = userService.List(); 
            if (response.Status == Message.ERROR)
            {
                return new Response(Message.ERROR, null);
            }
            return new Response(Message.SUCCESS, response.Result);
        }

        public Response Create(UserSchema user)
        {
            Response response = userService.Create(user);
            if (response.Status == Message.ERROR)
            {
                return new Response(Message.ERROR, null);
            }
            return new Response(Message.SUCCESS, response.Result);
        }

        public Response Delete(int id)
        {
            Response response = userService.Delete(id);
            if (response.Status == Message.ERROR)
            {
                return new Response(Message.ERROR, null);
            }
            return new Response(Message.SUCCESS, response.Result);
        }

    }
}
