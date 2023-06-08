using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseNetCore.Src.Services.Schemas
{
  public class UserRole
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public User User { get; set; }
    public Role Role { get; set; }
  }
}
