using Base.Core.Schemas;
using Base.Core;

namespace Base.Services
{

    public interface IUserGroup : IBaseService<UsersGroups>
    {
    }

    public class UserGroupService : BaseService<UsersGroups, DataContext>, IUserGroup
    {

        private readonly DataContext context;
        public UserGroupService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }
    }
}
