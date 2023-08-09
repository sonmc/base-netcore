using Base.Core;
using Base.Core.Schemas;
using Base.Services.Base;

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
            this.context = _ctx;
        } 
    }
}
