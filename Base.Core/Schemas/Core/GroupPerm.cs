using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Core.Schemas
{
    public class GroupPerm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int GroupId { get; set; }
        public int PermId { get; set; }

        public Group Group { get; set; }
        public Perm Perm { get; set; }
    }
}
