using Base.Core.Schemas;
using Base.Utils;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Base.Services;

namespace Base.Application.UseCases
{
    [ApiController]
    [Route("api/groups")]
    public class CrudGroupController : ControllerBase
    {
        private readonly IMapper _mapper;
        CrudGroupFlow workFlow;
        public CrudGroupController(IMapper mapper)
        {
            _mapper = mapper;
            workFlow = new CrudGroupFlow(new UnitOfWork());
        }


        [HttpGet(Name = "ListGroup_1")]
        public async Task<IActionResult> List(string sortName = "Id", string sortType = "ASC", int cursor = 0, int pageSize = 10)
        {
            Response response = workFlow.List();
            List<GroupSchema> items = (List<GroupSchema>)response.Result;
            CtrlUtil.ApplySort<GroupSchema, string>(ref items, sortName, sortType);
            ResponsePresenter res = CtrlUtil.ApplyPaging<GroupSchema, string>(cursor, pageSize, items);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpPost(Name = "CreateGroup_1")]
        public async Task<IActionResult> Create([FromBody] CreateGroupPresenter model)
        {
            GroupSchema group = _mapper.Map<GroupSchema>(model);
            Response response = await workFlow.Create(group);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpPut(Name = "UpdateGroup_1")]
        public async Task<IActionResult> Update([FromBody] UpdateGroupPresenter model)
        {
            GroupSchema group = _mapper.Map<GroupSchema>(model);
            Response response = await workFlow.Update(group);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpDelete(Name = "DeleteGroup_1")]
        public async Task<IActionResult> Deletes(int[] ids)
        {
            Response response = workFlow.Deletes(ids);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
