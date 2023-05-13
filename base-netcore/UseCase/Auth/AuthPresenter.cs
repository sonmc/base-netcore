using System.ComponentModel.DataAnnotations;
namespace base_netcore.UseCase.Auth
{
    public class AuthPresenter
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
