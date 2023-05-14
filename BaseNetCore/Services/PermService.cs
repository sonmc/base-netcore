 
using BaseNetCore.Infrastructure.Helper;
using BaseNetCore.Infrastructure.Repositories;

namespace BaseNetCore.Services
{
    public interface IPermService
    {
        Response Get();
    }

    public class PermService : IPermService
    {
        private readonly IPermRepository repository;

        public PermService(IPermRepository repository)
        {
            this.repository = repository;
        }

        public Response Get()
        {
            throw new NotImplementedException();
        }
    }
}
