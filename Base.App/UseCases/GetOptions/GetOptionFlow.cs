using Base.Services;
using Base.Utils;

namespace Base.App.UseCases
{
    public class GetOptionFlow
    {
        private readonly IUnitOfWork uow;
        public GetOptionFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public Response GetOptions()
        {
            var users = uow.Users.FindAll();
            return new Response(Message.SUCCESS, users);
        }


    }
}
