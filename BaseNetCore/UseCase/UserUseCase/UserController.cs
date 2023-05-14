using BaseNetCore.Infrastructure.Helper;
using BaseNetCore.Infrastructure.Helper.Constant;
using BaseNetCore.Utils;
using Microsoft.AspNetCore.Mvc; 

namespace BaseNetCore.UseCase.UserUseCase
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {

        readonly IUserFlow flow;

        public UserController(IUserFlow _flow)
        {
            flow = _flow;
        }

        [HttpGet("get-current-user")]
        public IActionResult GetCurentUser()
        {
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
