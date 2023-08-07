using Base.Core.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Base.Core.Database
{
    public class Seeding
    {
        public static async void SyncAllRouter(ModelBuilder modelBuilder)
        {
            var perm = new PermSchema { Id = 1, Title = "Test", Action = "", Module = "", ProfileTypes = "[]" };
            modelBuilder.Entity<PermSchema>().HasData(perm);
        }

        public static async void Init(ModelBuilder modelBuilder)
        {
            //var perm = new PermSchema { Id = 1, Title = "Test" };
            //modelBuilder.Entity<PermSchema>().HasData(perm);
        }
    }
}
