using Base.Core.Schemas;
using Base.Core;

namespace Base.Services
{

    public interface IUserGroup : IBaseService<UsersGroups>
    {
       UsersGroups VerityUserGroup(UsersGroups ug);
    }

    public class UserGroupService : BaseService<UsersGroups, DataContext>, IUserGroup
    {

        private readonly DataContext context;
        public UserGroupService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }

        public UsersGroups VerityUserGroup(UsersGroups ug)
        {
            var usersGroups = context.UsersGroups.ToList();
            foreach (var urg in usersGroups)
            {
                if (urg.GroupId == ug.GroupId && urg.UserId == ug.UserId)
                {
                    return null;
                }
            }
            return ug;
        }
    }
}
