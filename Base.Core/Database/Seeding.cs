using Base.Business.Rule;
using Base.Core.Schemas;
using Base.Utils;
using Microsoft.EntityFrameworkCore;
using static Base.Utils.CtrlUtil;

namespace Base.Core.Database
{
    public class Seeding
    {

        public static async void Init(ModelBuilder modelBuilder)
        {
            SeedGroup(modelBuilder);
            SeedUser(modelBuilder);
        }

        public static void SeedGroup(ModelBuilder modelBuilder)
        {
            Group admin = new Group { Id = 1, Title = "Admin", Description = "", ProfileType = RoleType.ADMIN_PROFILE };
            Group staff = new Group { Id = 2, Title = "Staff", Description = "", ProfileType = RoleType.STAFF_PROFILE };

            modelBuilder.Entity<Group>().HasData(admin);
            modelBuilder.Entity<Group>().HasData(staff);
        }


        public static void SeedUser(ModelBuilder modelBuilder)
        {
            string defaultPassword = JwtUtil.MD5Hash(UserRule.DEFAULT_PASSWORD);

            User userAdmin = new User { Id = 1, UserName = "admin", Password = defaultPassword, Email = "admin@gmail.com", GroupIds = RoleType.ADMIN_PROFILE };
            modelBuilder.Entity<User>().HasData(userAdmin);

            User userStaff = new User { Id = 2, UserName = "staff", Password = defaultPassword, Email = "staff@gmail.com", GroupIds = RoleType.STAFF_PROFILE };
            modelBuilder.Entity<User>().HasData(userStaff);
        }
    }
}
