using System.ComponentModel.DataAnnotations;
namespace Base.App.UseCases
{
  public class AuthPresenter
  {
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
