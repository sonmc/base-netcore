using Base.Core;
using Base.Core.Schemas;

namespace Base.Services
{

    public interface IGroup : IBaseService<Group>
    {
    }

    public class GroupService : BaseService<Group, DataContext>, IGroup
    {

        private readonly DataContext context;
        public GroupService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }
    }
}
