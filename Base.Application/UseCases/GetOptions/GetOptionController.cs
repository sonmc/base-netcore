using Base.Services;
using Base.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Base.Application.UseCases
{
    [ApiController]
    [Route("api")]
    public class GetOptionController : ControllerBase
    {
        GetOptionFlow workFlow;
        public GetOptionController()
        {
            workFlow = new GetOptionFlow(new UnitOfWork());
        }

        [HttpGet("options", Name = "Options_")]
        public IActionResult GetOption()
        {
            Response response = workFlow.GetOptions();
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response.Result);
        }
    }
}
