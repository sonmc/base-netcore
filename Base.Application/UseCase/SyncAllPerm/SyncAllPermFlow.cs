
using Base.Services;

namespace Base.Application.UseCase.SyncAllPerm
{
    public class SyncAllPermFlow
    {
        private readonly IUnitOfWork uow;
        public SyncAllPermFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public Response SyncAllPerm()
        {
            Response response = new Response();
            return response;
        }
  
    }
}
