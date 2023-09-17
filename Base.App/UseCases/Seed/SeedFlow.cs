
using Base.Business.Rule;
using Base.Core.Schemas;
using Base.Services;
using Base.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using static Base.Utils.CtrlUtil;

namespace Base.App.UseCases
{
    public class SeedFlow
    {
        private readonly IUnitOfWork uow;
        public SeedFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public async Task<Response> Seed(List<RouterPresenter> routers)
        {
            bool isSeeded = false;
            var executionStrategy = uow.CreateExecutionStrategy();

            await executionStrategy.Execute(async () =>
            {
                using IDbContextTransaction transaction = uow.BeginTransaction();
                try
                {
                    SeedGroup();
                    SeedUser();
                    List<Perm> perms = CreatePerms(routers);
                    List<Group> groups = CreateGroupPerm(perms);
                    AddUserToGroup(groups);
                    isSeeded = true;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    isSeeded = false;
                    transaction.Rollback();
                    throw;
                }
            });
            Response response = new Response("success", isSeeded);
            return response;
        }

        private List<Group> CreateGroupPerm(List<Perm> perms)
        {
            List<Group> groups = uow.Groups.FindAll();
            List<GroupPerm> groupsPerms = new();
            foreach (var perm in perms)
            {
                foreach (var group in groups)
                {
                    if (group.ProfileType.Contains(perm.ProfileTypes) || perm.ProfileTypes == CtrlUtil.RoleType.PUBLIC_PROFILE)
                    {
                        GroupPerm gp = new GroupPerm();
                        gp.Perm = perm;
                        gp.Group = group;
                        groupsPerms.Add(gp);
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
                Perm permSchema = new Perm();
                permSchema.Action = router.Template.Replace("api/", "");
                permSchema.Title = StrUtil.ConvertCamelToTitle(router.Name.Split('_')[0]);
                permSchema.ProfileTypes = "[" + router.Name.Split('_')[1] + "]";
                permSchema.Module = router.Module;
                perms.Add(permSchema);
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
                        usersGroups.Add(ug);
                    }
                }
            }
            uow.UsersGroups.Creates(usersGroups);
        }

        private void SeedGroup()
        {
            Group admin = new Group { Id = 1, Title = "Admin", Description = "", ProfileType = RoleType.ADMIN_PROFILE };
            Group staff = new Group { Id = 2, Title = "Staff", Description = "", ProfileType = RoleType.STAFF_PROFILE };

            List<Group> groups = new List<Group> { admin, staff };
            uow.Groups.Creates(groups);
        }

        private void SeedUser()
        {
            string defaultPassword = JwtUtil.MD5Hash(UserRule.DEFAULT_PASSWORD);
            User userAdmin = new User { Id = 1, UserName = "admin", Password = defaultPassword, Email = "admin@gmail.com", GroupIds = RoleType.ADMIN_PROFILE };
            User userStaff = new User { Id = 2, UserName = "staff", Password = defaultPassword, Email = "staff@gmail.com", GroupIds = RoleType.STAFF_PROFILE };
            List<User> users = new List<User> { userAdmin, userStaff };
            uow.Users.Creates(users);
        }
    }
}
