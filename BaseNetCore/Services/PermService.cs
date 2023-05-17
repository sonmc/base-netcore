 
using BaseNetCore.Infrastructure.Helper; 
using BaseNetCore.Infrastructure.Schemas;

namespace BaseNetCore.Services
{
    public interface IPermService
    {
        Response Get();
    }

    public class PermService : IPermService
    {
        private readonly DataContext context;
        public PermService()
        {
            this.context = new DataContext() ;
        }

        public Response Get()
        {
            throw new NotImplementedException();
        }
    }
}
