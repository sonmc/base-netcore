 
namespace base_netcore.Infrastructure.Schemas
{
    public class User: BaseSchema
    { 
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }
        public List<Role> Roles { get; set; }

    }

}
