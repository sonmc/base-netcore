using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Core.Schemas
{
  public class UsersGroups
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserGroupId { get; set; } 

    public UserSchema User { get; set; }
    public GroupSchema Group { get; set; }
  }
}
