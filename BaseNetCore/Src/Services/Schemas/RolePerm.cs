using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseNetCore.Src.Services.Schemas
{
  public class RolePerm
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
          
    public RoleSchema Role { get; set; }
    public Perm Permission { get; set; }
  }
}
