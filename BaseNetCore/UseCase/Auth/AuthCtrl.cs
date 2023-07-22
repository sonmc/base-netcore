﻿  
using Microsoft.AspNetCore.Mvc;
using Base.Utils;
using Base.Services;

namespace BaseNetCore.Src.UseCase.Auth
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
       
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthPresenter model)
        {

            AuthFlow flow = new AuthFlow(new UnitOfWork());
            Response response = flow.Login(model.Username, model.Password);
            if (response.Status == Message.ERROR)
            {
                return Unauthorized();
            }
            TokenPresenter token = (TokenPresenter)response.Result;
            CookieOptions cookieOptions = JwtUtil.GetConfigOption();
            Response.Cookies.Append(JwtUtil.ACCESS_TOKEN, token.AccessToken, cookieOptions);
            Response.Cookies.Append(JwtUtil.REFRESH_TOKEN, token.RefreshToken);
            return Ok();
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] TokenPresenter tokenParam)
        {
           
            if (string.IsNullOrWhiteSpace(tokenParam.RefreshToken))
            {
                return Unauthorized();
            }
            AuthFlow flow = new AuthFlow(new UnitOfWork());
            Response response = flow.RefreshToken(tokenParam.AccessToken, tokenParam.RefreshToken);
            if (response.Status == Message.ERROR)
            {
                return Unauthorized();
            }
            TokenPresenter token = (TokenPresenter)response.Result;
            CookieOptions cookieOptions = JwtUtil.GetConfigOption();
            Response.Cookies.Append(JwtUtil.ACCESS_TOKEN, token.AccessToken, cookieOptions);
            Response.Cookies.Append(JwtUtil.REFRESH_TOKEN, token.RefreshToken);
            return Ok();
        }

        [HttpGet("logout")]
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