
using Microsoft.AspNetCore.Mvc;
using Base.Utils;
using Base.Services;
using Microsoft.AspNetCore.Authorization;

namespace Base.App.UseCases
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        AuthFlow workFlow;
        public AuthController(IUnitOfWork uow)
        {
            workFlow = new AuthFlow(uow);
        }

        [HttpPost("login", Name = "Login_")]
        public IActionResult Login([FromBody] AuthPresenter model)
        {
            Response response = workFlow.Login(model.Username, model.Password);
            if (response.Status == Message.ERROR)
            {
                return Unauthorized();
            }
            TokenPresenter token = (TokenPresenter)response.Result;
            return Ok(token);
        }

        [HttpGet("refresh-token", Name = "RefreshToken_")]
        public IActionResult RefreshToken([FromBody] TokenPresenter tokenParam)
        {

            if (string.IsNullOrWhiteSpace(tokenParam.RefreshToken))
            {
                return Unauthorized();
            }

            Response response = workFlow.RefreshToken(tokenParam.AccessToken, tokenParam.RefreshToken);
            if (response.Status == Message.ERROR)
            {
                return Unauthorized();
            }
            TokenPresenter token = (TokenPresenter)response.Result; 
            return Ok(token);
        }

        [Authorize]
        [HttpGet("logout", Name = "Logout_")]
        public IActionResult Logout()
        {
            string token = Request.Headers[JwtUtil.ACCESS_TOKEN];
            if (token == null)
            {
                return Unauthorized();
            }
            Response.Cookies.Append(JwtUtil.ACCESS_TOKEN, null);
            return Ok();
        }
    }
}