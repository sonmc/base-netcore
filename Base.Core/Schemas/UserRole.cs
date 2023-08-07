using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Core.Schemas
{
  public class UserRole
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserRoleId { get; set; } 

    public UserSchema User { get; set; }
    public GroupSchema Role { get; set; }
  }
}
