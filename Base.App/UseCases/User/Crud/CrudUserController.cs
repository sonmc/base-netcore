using Base.Core.Schemas;
using Base.Utils;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Base.Services;

namespace Base.App.UseCases
{
    [ApiController]
    [Route("api/users")]
    public class CrudUserController : ControllerBase
    {
        private readonly IMapper _mapper;
        CrudUserFlow workFlow;
        public CrudUserController(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            workFlow = new CrudUserFlow(uow);
        }
         

        [HttpGet(Name = "ListUser_1")]
        public async Task<IActionResult> List(string sortName = "Id", string sortType = "ASC", int cursor = 0, int pageSize = 10)
        {
            Response response = workFlow.List();
            List<User> items = (List<User>)response.Result;
            CtrlUtil.ApplySort<User, string>(ref items, sortName, sortType);
            ResponsePresenter res = CtrlUtil.ApplyPaging<User, string>(cursor, pageSize, items);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            res.Items = CrudUserPresenter.PresentList((List<User>)res.Items);
            return Ok(res);
        }

        [HttpPost(Name = "CreateUser_1")]
        public async Task<IActionResult> Create([FromBody] CreateUserPresenter model)
        {
            User user = _mapper.Map<User>(model);
            Response response = await workFlow.Create(user);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpPut(Name = "UpdateUser_1")]
        public async Task<IActionResult> Update([FromBody] UpdateUserPresenter model)
        {
            User user = _mapper.Map<User>(model);
            Response response = await workFlow.Update(user);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpDelete(Name = "DeleteUser_1")]
        public async Task<IActionResult> Delete(int[] ids)
        {
            Response response = await workFlow.Deletes(ids);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
