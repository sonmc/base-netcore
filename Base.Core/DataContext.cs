
using Base.Core.Database;
using Base.Core.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Base.Core
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserSchema> Users { get; set; }
        public virtual DbSet<RoleSchema> Roles { get; set; }
        public virtual DbSet<Perm> Permissions { get; set; }
        public virtual DbSet<RolePerm> RolesPerms { get; set; }
        public virtual DbSet<UserRole> UsersRoles { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder
        )
        {
            var connectionString = "server=localhost;port=3307;database=base_core;uid=root;password=123456";
            optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("8.0.28-mysql"), 
                builder =>
                   {
                       builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                   });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Seeding.PermInit(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
