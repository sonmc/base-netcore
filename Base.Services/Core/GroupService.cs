using Base.Core;
using Base.Core.Schemas;

namespace Base.Services
{

    public interface IGroup : IBaseService<GroupSchema>
    {
    }

    public class GroupService : BaseService<GroupSchema, DataContext>, IGroup
    {

        private readonly DataContext context;
        public GroupService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }
    }
}
