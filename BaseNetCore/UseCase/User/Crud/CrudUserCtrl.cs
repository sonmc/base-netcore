using Base.Core.Schemas;
using Base.Utils;
using Base.Services;
using Microsoft.AspNetCore.Mvc; 

namespace BaseNetCore.Src.UseCase.User.Crud
{
    [ApiController]
    [Route("users")]
    public class CrudUserCtrl : ControllerBase
    {
        [HttpGet("get-current-user")]
        public IActionResult GetCurentUser()
        {
            CrudUserFlow flow = new CrudUserFlow(new UnitOfWork());
            string token = Request.Cookies[JwtUtil.ACCESS_TOKEN];
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

        [HttpGet]
        public async Task<IActionResult> List()
        {
            CrudUserFlow flow = new CrudUserFlow(new UnitOfWork());
            Response response = flow.List();
            List<UserSchema> items = (List<UserSchema>)response.Result;
            int cursor = 1;
            int pageSize = 10;
            string sortName = "UserName";
            string sortType = "asc";

            var result = await CtrlUtil.ApplySortAndPaging<UserSchema, string>(cursor, pageSize, items, sortName, sortType);

            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserSchema user)
        {
            CrudUserFlow flow = new CrudUserFlow(new UnitOfWork());
            Response response = flow.Create(user);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            CrudUserFlow flow = new CrudUserFlow(new UnitOfWork());
            Response response = flow.Delete(id);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
