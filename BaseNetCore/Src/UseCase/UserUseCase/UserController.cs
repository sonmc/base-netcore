using BaseNetCore.Src.Infrastructure.Helper;
using BaseNetCore.Src.Infrastructure.Helper.Constant;
using BaseNetCore.Src.Services;
using BaseNetCore.Src.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BaseNetCore.Src.UseCase.UserUseCase
{
  [ApiController]
  [Route("users")]
  public class UserController : ControllerBase
  {


    [HttpGet("get-current-user")]
    public IActionResult GetCurentUser()
    {
      UserFlow flow = new UserFlow(new UserService());
      string token = Request.Cookies[Jwt.ACCESS_TOKEN];
      if (string.IsNullOrEmpty(token))
      {
        return Unauthorized();
      }
      Response response = flow.GetCurrentUser(token);
      if (response.Status == Message.ERROR)
      {
        return Unauthorized();
      }
      return Ok(response.Result);
    }
  }
}
