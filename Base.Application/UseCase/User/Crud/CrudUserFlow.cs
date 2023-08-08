using Base.Core.Schemas;
using Base.Utils;
using Base.Services;

namespace Base.Application.UseCase.User
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
            UserSchema user = uow.Users.Get(id);
            return new Response(Message.SUCCESS, user);
        }

        public Response List()
        {
            var users = uow.Users.GetAll();
            return new Response(Message.SUCCESS, users);
        }

        public async Task<Response> Create(UserSchema user)
        {
            var result =  uow.Users.Add(user); 
            return new Response(Message.SUCCESS, result);
        }

        public async Task<Response> Update(UserSchema user)
        {
            var result = uow.Users.Update(user);
            return new Response(Message.SUCCESS, result);
        }

        public Response Delete(int id)
        {
            var result = uow.Users.Delete(id); 
            return new Response(Message.SUCCESS, result);
        }

    }
}
