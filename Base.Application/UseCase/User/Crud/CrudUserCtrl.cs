using Base.Core.Schemas;
using Base.Utils;
using Base.Services;
using Microsoft.AspNetCore.Mvc;
using Base.Application.UseCase.User.Crud.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Base.Application.UseCase.User.Crud
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class CrudUserCtrl : ControllerBase
    {
        private readonly IMapper _mapper;
        public CrudUserCtrl(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("get-current-user")]
        public IActionResult GetCurentUser()
        {
            CrudUserFlow flow = new CrudUserFlow(new ZUnitOfWork());
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
        public async Task<IActionResult> List(string sortName, string sortType = "asc", int cursor = 0, int pageSize = 10)
        {
            CrudUserFlow flow = new CrudUserFlow(new ZUnitOfWork());
            Response response = flow.List();
            List<UserSchema> items = (List<UserSchema>)response.Result;
            CtrlUtil.ApplySort<UserSchema, string>(ref items, sortName, sortType);
            ResponsePresenter res = CtrlUtil.ApplyPaging<UserSchema, string>(cursor, pageSize, items); 
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            res.Items = CrudUserPresenter.PresentList((List<UserSchema>)res.Items);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserModel model)
        {
            CrudUserFlow flow = new CrudUserFlow(new ZUnitOfWork());
            UserSchema user = _mapper.Map<UserSchema>(model);
            Response response = await flow.Create(user);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserModel model)
        {
            CrudUserFlow flow = new CrudUserFlow(new ZUnitOfWork());
            UserSchema user = _mapper.Map<UserSchema>(model);
            Response response = await flow.Update(user);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            CrudUserFlow flow = new CrudUserFlow(new ZUnitOfWork());
            Response response = flow.Delete(id);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
