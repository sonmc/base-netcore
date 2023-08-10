

using Base.Application.UseCase.User.Crud;

namespace Base.Application.Helper
{
    public class MappingConfig
    {
        public static void AutoMapperConfig(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CreateUserMapping));
        }
    }
}
