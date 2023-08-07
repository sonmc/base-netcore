using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Core.Schemas
{
  public class GroupPerm
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
          
    public GroupSchema Group { get; set; }
    public PermSchema Perm { get; set; }
  }
}
