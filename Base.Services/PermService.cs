using Base.Core;
using Base.Core.Schemas;
using Base.Services.Base;

namespace Base.Services
{

    public interface IPerm : IBaseService<PermSchema>
    {
        List<PermSchema> Creates(List<PermSchema> perms);
    }

    public class PermService : BaseService<PermSchema, DataContext>, IPerm
    {

        private readonly DataContext context;
        public PermService(DataContext _ctx) : base(_ctx)
        {
            this.context = _ctx;
        }

        public List<PermSchema> Creates(List<PermSchema> perms)
        {
            context.Perms.AddRange(perms);
            context.SaveChanges();
            return perms;
        }
    }
}
