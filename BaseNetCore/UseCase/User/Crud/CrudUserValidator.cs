using System.ComponentModel.DataAnnotations;

namespace BaseNetCore.Src.UseCase.User.Crud
{
    public class CrudUserValidator
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
