using Base.Services;
using Base.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Base.App.UseCases
{
    [ApiController]
    [Route("api")]
    public class GetOptionController : ControllerBase
    {
        GetOptionFlow workFlow;
        public GetOptionController(IUnitOfWork uow)
        {
            workFlow = new GetOptionFlow(uow);
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
