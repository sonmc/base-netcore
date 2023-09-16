using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Base.Core.Schemas
{
    public class Perm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public string ProfileTypes { get; set; }

    }
}
