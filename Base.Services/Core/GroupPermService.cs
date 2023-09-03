using Base.Core;
using Base.Core.Schemas;

namespace Base.Services
{

    public interface IGroupPerm : IBaseService<GroupPerm>
    {
    }

    public class GroupPermService : BaseService<GroupPerm, DataContext>, IGroupPerm
    {

        private readonly DataContext context;
        public GroupPermService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }

    }
}
