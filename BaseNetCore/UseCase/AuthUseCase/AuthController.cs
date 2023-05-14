using BaseNetCore.Utils;
using BaseNetCore.Infrastructure.Helper;
using BaseNetCore.Infrastructure.Helper.Constant;
using Microsoft.AspNetCore.Mvc; 

namespace BaseNetCore.UseCase.AuthUseCase
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        readonly IAuthFlow flow;

        public AuthController(IAuthFlow _flow)
        {
            flow = _flow;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthPresenter model)
        {
            Response response = flow.Login(model.Username, model.Password);
            if (response.Status == Message.ERROR)
            {
                return Unauthorized();
            } 
            TokenPresenter token = (TokenPresenter)response.Result;
            CookieOptions cookieOptions = Jwt.GetConfigOption();
            Response.Cookies.Append(Jwt.ACCESS_TOKEN, token.AccessToken, cookieOptions);
            Response.Cookies.Append(Jwt.REFRESH_TOKEN, token.RefreshToken);

            return Ok();
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] TokenPresenter tokenParam)
        {
            if (string.IsNullOrWhiteSpace(tokenParam.AccessToken) || string.IsNullOrWhiteSpace(tokenParam.RefreshToken))
            {
                return Unauthorized();
            } 
            Response response = flow.RefreshToken(tokenParam.AccessToken, tokenParam.RefreshToken);
            if (response.Status == Message.ERROR)
            {
                return Unauthorized();
            }
            TokenPresenter token = (TokenPresenter)response.Result;
            CookieOptions cookieOptions = Jwt.GetConfigOption();
            Response.Cookies.Append(Jwt.ACCESS_TOKEN, token.AccessToken, cookieOptions);
            Response.Cookies.Append(Jwt.REFRESH_TOKEN, token.RefreshToken);
            return Ok();
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            string token = Request.Headers[Jwt.ACCESS_TOKEN];
            Response.Cookies.Append(Jwt.ACCESS_TOKEN, null); 
            return Ok();
        }
    }
}