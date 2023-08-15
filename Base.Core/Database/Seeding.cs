﻿using Base.Business.Rule;
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
            GroupSchema admin = new GroupSchema { Id = 1, Title = "Admin", Description = "", ProfileType = RoleType.ADMIN_PROFILE };
            GroupSchema staff = new GroupSchema { Id = 2, Title = "Staff", Description = "", ProfileType = RoleType.STAFF_PROFILE };

            modelBuilder.Entity<GroupSchema>().HasData(admin);
            modelBuilder.Entity<GroupSchema>().HasData(staff);
        }


        public static void SeedUser(ModelBuilder modelBuilder)
        {
            string defaultPassword = JwtUtil.MD5Hash(UserRule.DEFAULT_PASSWORD);

            UserSchema userAdmin = new UserSchema { Id = 1, UserName = "admin", Password = defaultPassword, Email = "admin@gmail.com", GroupIds = RoleType.ADMIN_PROFILE };
            modelBuilder.Entity<UserSchema>().HasData(userAdmin);

            UserSchema userStaff = new UserSchema { Id = 2, UserName = "staff", Password = defaultPassword, Email = "staff@gmail.com", GroupIds = RoleType.STAFF_PROFILE };
            modelBuilder.Entity<UserSchema>().HasData(userStaff);
        }
    }
}