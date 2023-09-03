using Base.Core;
using Base.Core.Schemas;

namespace Base.Services
{

    public interface IPerm : IBaseService<PermSchema>
    {
    }

    public class PermService : BaseService<PermSchema, DataContext>, IPerm
    {

        private readonly DataContext context;
        public PermService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }

    }
}
