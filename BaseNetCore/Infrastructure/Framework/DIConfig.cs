using BaseNetCore.Infrastructure.Repositories;
using BaseNetCore.Services;
using BaseNetCore.UseCase.AuthUseCase;
using BaseNetCore.UseCase.UserUseCase;

namespace BaseNetCore.Infrastructure.Framework
{
    public class DIConfig
    {
        public static void Configuration(IServiceCollection services)
        { 
            services.AddScoped<IAuthFlow, AuthFlow>();
            services.AddScoped<IUserFlow, UserFlow>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<IPermRepository, PermRepository>();
        }
    }
}
