using Base.Core.Schemas;
using Base.Core;
using Base.Services.Base;

namespace Base.Services
{

    public interface IUserGroup : IBaseService<UsersGroups>
    {
        List<UsersGroups> Creates(List<UsersGroups> usersGroups);
    }

    public class UserGroupService : BaseService<UsersGroups, DataContext>, IUserGroup
    {

        private readonly DataContext context;
        public UserGroupService(DataContext _ctx) : base(_ctx)
        {
            this.context = _ctx;
        }

        public List<UsersGroups> Creates(List<UsersGroups> usersGroups)
        {
            context.UsersGroups.AddRange(usersGroups);
            context.SaveChanges();
            return usersGroups;
        }
    }
}
