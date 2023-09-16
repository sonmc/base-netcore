using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Core.Schemas
{
    public class UsersGroups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Group Group { get; set; }
    }
}
