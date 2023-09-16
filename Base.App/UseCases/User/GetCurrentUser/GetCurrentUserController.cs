using Base.Utils;
using Microsoft.AspNetCore.Mvc;
using Base.Services;

namespace Base.App.UseCases
{
    [ApiController]
    [Route("api/users")]
    public class GetCurrentUserController : ControllerBase
    {
        GetCurrentUserFlow workFlow;
        public GetCurrentUserController(IUnitOfWork uow)
        {
            workFlow = new GetCurrentUserFlow(uow);
        }

        [HttpGet("get-current-user", Name = "GetCurentUser_1")]
        public IActionResult GetCurentUser()
        {
            string token = Request.Cookies[JwtUtil.ACCESS_TOKEN];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            Response response = workFlow.GetCurrentUser(token);
            if (response.Status == Message.ERROR)
            {
                return Unauthorized();
            }
            return Ok(response.Result);
        }
    }
}
