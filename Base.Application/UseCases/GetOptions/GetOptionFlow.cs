using Base.Application.UseCase; 
using Base.Services.Base;
using Base.Utils;

namespace Base.Application.UseCases.GetOptions
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
            var users = uow.Users.GetAll();
            return new Response(Message.SUCCESS, users);
        }


    }
}
