using Base.Core.Schemas;
using Base.Utils;
using Base.Services;

namespace BaseNetCore.Src.UseCase.User
{

    public class CrudUserFlow
    {
        private readonly IUnitOfWork uow;
        public CrudUserFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public Response GetCurrentUser(string token)
        {
            string userCredentialString = JwtUtil.GetIdByToken(token);
            int id = 0;
            if (!Int32.TryParse(userCredentialString, out id))
            {
                return new Response(Message.ERROR, null);
            }
            Response response = uow.User.Get(id);

            if (response.Status == Message.ERROR)
            {
                return new Response(Message.ERROR, null);
            }
            UserSchema user = (UserSchema)response.Result;
            return new Response(Message.SUCCESS, user);
        }

        public Response List()
        {
            Response response = uow.User.List();
            if (response.Status == Message.ERROR)
            {
                return new Response(Message.ERROR, null);
            }
            return new Response(Message.SUCCESS, response.Result);
        }

        public Response Create(UserSchema user)
        {
            Response response = uow.User.Create(user);
            if (response.Status == Message.ERROR)
            {
                return new Response(Message.ERROR, null);
            }
            return new Response(Message.SUCCESS, response.Result);
        }

        public Response Delete(int id)
        {
            Response response = uow.User.Delete(id);
            if (response.Status == Message.ERROR)
            {
                return new Response(Message.ERROR, null);
            }
            return new Response(Message.SUCCESS, response.Result);
        }

    }
}
