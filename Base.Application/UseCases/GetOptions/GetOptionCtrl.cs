 
using Base.Application.UseCase;
using Base.Services.Base;
using Base.Utils;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Authorization;

namespace Base.Application.UseCases.GetOptions
{

    [Authorize]
    [ApiController]
    [Route("api")]
    public class GetOptionCtrl : ControllerBase
    {
        GetOptionFlow workFlow;
        public GetOptionCtrl()
        {
            workFlow = new GetOptionFlow(new UnitOfWork());
        }

        [HttpGet("options", Name = "OPTIONS_")]
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
