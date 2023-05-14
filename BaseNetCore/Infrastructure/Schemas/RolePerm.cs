using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseNetCore.Infrastructure.Schemas
{
    public class RolePerm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int RoleId { get; set; }
        public int PermId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
