using System.ComponentModel.DataAnnotations;
namespace BaseNetCore.UseCase.AuthUseCase
{
    public class AuthPresenter
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
