
using Base.Core.Schemas;
using Base.Services;
using Base.Utils;

namespace Base.App.UseCases
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
            List<Perm> perms = CreatePerms(routers);
            List<Group> groups = CreateGroupPerm(perms);
            AddUserToGroup(groups);
            Response response = new Response("success", perms);
            return response;
        }

        private List<Group> CreateGroupPerm(List<Perm> perms)
        {
            List<Group> groups = uow.Groups.FindAll();
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
                        GroupPerm groupPerm = uow.GroupsPerms.VerifyGroupPerm(gp);
                        if (groupPerm == null) { continue; }
                        groupsPerms.Add(groupPerm);
                    }
                }
            }
            uow.GroupsPerms.Creates(groupsPerms);
            return groups;
        }

        private List<Perm> CreatePerms(List<RouterPresenter> routers)
        {
            List<Perm> perms = new List<Perm>();
            foreach (var router in routers)
            {
                Perm Perm = new Perm();
                Perm.Action = router.Template.Replace("api/", "");
                Perm.Title = StrUtil.ConvertCamelToTitle(router.Name.Split('_')[0]);
                Perm.ProfileTypes = "[" + router.Name.Split('_')[1] + "]";
                Perm.Module = router.Module;
                Perm permission = uow.Perms.VerifyPerms(Perm);
                if (permission == null)
                {
                    continue;
                }
                perms.Add(permission);
            }

            perms = uow.Perms.Creates(perms);
            return perms;
        }

        private void AddUserToGroup(List<Group> groups)
        {
            List<UsersGroups> usersGroups = new List<UsersGroups>();
            List<User> users = uow.Users.FindAll();
            foreach (var user in users)
            {
                foreach (var group in groups)
                {
                    if (user.GroupIds.Contains(group.ProfileType))
                    {
                        UsersGroups ug = new UsersGroups();
                        ug.User = user;
                        ug.Group = group;
                        UsersGroups userGroup = uow.UsersGroups.VerityUserGroup(ug);
                        if (userGroup == null) { continue; }
                        usersGroups.Add(userGroup);
                    }
                }
            }
            uow.UsersGroups.Creates(usersGroups);
        }
    }
}
