using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseNetCore.Src.Services.Schemas
{
  public class UserRole
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserRoleId { get; set; } 

    public UserSchema User { get; set; }
    public RoleSchema Role { get; set; }
  }
}
