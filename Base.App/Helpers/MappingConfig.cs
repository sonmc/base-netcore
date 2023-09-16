

using Base.App.UseCases;

namespace Base.App.Helpers
{
    public class MappingConfig
    {
        public static void AutoMapperConfig(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CreateUserMapping));
        }
    }
}
