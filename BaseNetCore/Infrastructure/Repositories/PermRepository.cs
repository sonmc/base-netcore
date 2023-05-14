using BaseNetCore.Infrastructure.Schemas;

namespace BaseNetCore.Infrastructure.Repositories
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
