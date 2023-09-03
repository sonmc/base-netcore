using Base.Core;
using Base.Core.Schemas;

namespace Base.Services
{
    public interface IPerm : IBaseService<PermSchema>
    {
        PermSchema VerifyPerms(PermSchema p);
    }

    public class PermService : BaseService<PermSchema, DataContext>, IPerm
    {
        private readonly DataContext context;
        public PermService(DataContext _ctx) : base(_ctx)
        {
            context = _ctx;
        }

        public PermSchema VerifyPerms(PermSchema p)
        {
            var permissions = context.Perms.ToList();
            foreach (var perm in permissions)
            {
                if (p.Module.Equals(perm.Module) && p.Title.Equals(perm.Title) && p.Action.Equals(perm.Action))
                {
                    return null;
                }
            }
            return p;
        }
    }
}
