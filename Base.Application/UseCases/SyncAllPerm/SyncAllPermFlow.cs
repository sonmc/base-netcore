
using Base.Core.Schemas;
using Base.Services;
using Base.Services.Base;
using Base.Utils;

namespace Base.Application.UseCases
{
    public class SyncAllPermFlow
    {
        private readonly IUnitOfWork uow;
        public SyncAllPermFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public Response SyncAllPerm(List<RouterPresenter> routers)
        {
            List<PermSchema> perms = CreatePerms(routers);
            List<GroupSchema> groups = CreateGroupPerm(perms);
            AddUserToGroup(groups);
            Response response = new Response("success", perms);
            return response;
        }

        private List<GroupSchema> CreateGroupPerm(List<PermSchema> perms)
        {
            List<GroupSchema> groups = uow.Groups.FindAll();
            List<GroupPerm> groupsPerms = new();
            foreach (var perm in perms)
            {
                for (int i = 0; i < groups.Count; i++)
                {
                    if (groups[i].ProfileType.Contains(perm.ProfileTypes) || perm.ProfileTypes == CtrlUtil.RoleType.PUBLIC_PROFILE)
                    {
                        GroupPerm gp = new GroupPerm();
                        gp.Perm = perm;
                        gp.Group = groups[i];
                        groupsPerms.Add(gp);
                    }
                }

            }
            uow.GroupsPerms.Creates(groupsPerms);

            return groups;
        }

        private List<PermSchema> CreatePerms(List<RouterPresenter> routers)
        {
            List<PermSchema> perms = new List<PermSchema>();
            foreach (var router in routers)
            {
                PermSchema permSchema = new PermSchema();
                permSchema.Action = router.Template.Replace("api/", "");
                permSchema.Title = StrUtil.ConvertCamelToTitle(router.Name.Split('_')[0]);
                permSchema.ProfileTypes = "[" + router.Name.Split('_')[1] + "]";
                permSchema.Module = router.Module;
                perms.Add(permSchema);
            }
            perms = uow.Perms.Creates(perms);
            return perms;
        }

        private void AddUserToGroup(List<GroupSchema> groups)
        {
            List<UsersGroups> usersGroups = new List<UsersGroups>();
            List<UserSchema> users = uow.Users.FindAll();
            foreach (var user in users)
            {
                foreach (var group in groups)
                {
                    if (user.GroupIds.Contains(group.ProfileType))
                    {
                        UsersGroups ug = new UsersGroups();
                        ug.User = user;
                        ug.Group = group;
                        usersGroups.Add(ug);
                    }
                }
            }
            uow.UsersGroups.Creates(usersGroups);
        }
    }
}
