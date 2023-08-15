
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
        public virtual DbSet<GroupSchema> Groups { get; set; }
        public virtual DbSet<PermSchema> Perms { get; set; }
        public virtual DbSet<GroupPerm> GroupsPerms { get; set; }
        public virtual DbSet<UsersGroups> UsersGroups { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder
        )
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "server=localhost;port=3306;database=base_core_v1;uid=root;password=123456";
                optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("8.0.28-mysql"),
                    builder =>
                    {
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    });
                base.OnConfiguring(optionsBuilder);
            } 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Seeding.Init(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
