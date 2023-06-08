using BaseNetCore.Src.Services.Schemas;
using Microsoft.EntityFrameworkCore;

namespace BaseNetCore.Helper
{
    public class Seeding
    {
        public static void PermInit(ModelBuilder modelBuilder)
        {
            var perm = new Perm { Id = 1, Label = "Test", Name = "Test" };
            modelBuilder.Entity<Perm>().HasData(perm);
        }
    }
}
