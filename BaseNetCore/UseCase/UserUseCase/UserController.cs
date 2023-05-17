using BaseNetCore.Infrastructure.Helper;
using BaseNetCore.Infrastructure.Helper.Constant;
using BaseNetCore.Services;
using BaseNetCore.Utils;
using Microsoft.AspNetCore.Mvc; 

namespace BaseNetCore.UseCase.UserUseCase
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
