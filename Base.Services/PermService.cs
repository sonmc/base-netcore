
using Base.Core;
using Base.Utils;


namespace Base.Services
{
    public interface IPermService
    { 
    }

    public class PermService : IPermService
    {
        private readonly DataContext context;
        public PermService()
        {
            this.context = new DataContext();
        }
         
    }
}
