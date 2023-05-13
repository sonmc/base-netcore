using base_netcore.Infrastructure.Repositories;
using base_netcore.Services; 

namespace base_netcore.Infrastructure.Framework.Config
{
    public class DIConfig
    {
        public static void Configuration(IServiceCollection services)
        {

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IPermRepository, PermRepository>();
        }
    }
}
