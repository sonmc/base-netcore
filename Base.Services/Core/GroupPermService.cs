using Base.Core;
using Base.Core.Schemas;

namespace Base.Services
{

    public interface IGroupPerm : IBaseService<GroupPerm>
    {
        GroupPerm VerifyGroupPerm(GroupPerm gp);
    }

    public class GroupPermService : BaseService<GroupPerm, DataContext>, IGroupPerm
    {

        private readonly DataContext context;
        public GroupPermService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }

        public GroupPerm VerifyGroupPerm(GroupPerm gp)
        {
            var groupPerms = context.GroupsPerms.ToList();
            foreach (var grp in groupPerms)
            {
                if (grp.GroupId == gp.GroupId && grp.PermId == gp.PermId)
                {
                    return null;
                }
            }
            return gp;
        }
    }
}
