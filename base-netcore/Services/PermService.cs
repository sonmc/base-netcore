using base_netcore.Infrastructure.Helper;
using base_netcore.Infrastructure.Helper.Constant;
using base_netcore.Infrastructure.Repositories;
using base_netcore.Infrastructure.Schemas;

namespace base_netcore.Services
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
        
    }
}
