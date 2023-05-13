using base_netcore.Infrastructure.Schemas;

namespace base_netcore.Infrastructure.Repositories
{

    public interface IPermRepository : IBaseRepository<Permission>
    {
         
    }

    public class PermRepository : BaseRepository<Permission, DataContext>, IPermRepository
    {
        DataContext dbContext;

        public PermRepository() { }

        public PermRepository(DataContext context) : base(context)
        {
            this.dbContext = context;
        }

     
    }
}
