using base_netcore.Infrastructure.Helper; 
using base_netcore.UseCase.Auth;
using Microsoft.AspNetCore.Mvc;

namespace base_netcore.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        readonly IAuthFlow flow;

        public AuthController(IAuthFlow _flow) { 
            flow = _flow;
        }
         
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthPresenter model)
        {
            Response response = flow.Login(model.Username, model.Password);
            
            return Ok(response.Result);
        }
    }
}