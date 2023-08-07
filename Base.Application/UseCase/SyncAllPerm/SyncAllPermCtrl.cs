
using Microsoft.AspNetCore.Mvc;
using Base.Services;

namespace Base.Application.UseCase.SyncAllPerm
{
    [ApiController]
    [Route("api/sync-all-perm")]
    public class SyncAllPermController : ControllerBase
    {
        SyncAllPermFlow flow;
        public SyncAllPermController()
        {
            flow = new SyncAllPermFlow(new ZUnitOfWork());
        }

        [HttpGet()]
        public IActionResult SyncAllPerm()
        {
            Response response = flow.SyncAllPerm();
            return Ok(response);
        }
    }
}