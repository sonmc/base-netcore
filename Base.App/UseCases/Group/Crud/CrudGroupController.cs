using Base.Core.Schemas;
using Base.Utils;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Base.Services;

namespace Base.App.UseCases
{
    [ApiController]
    [Route("api/groups")]
    public class CrudGroupController : ControllerBase
    {
        private readonly IMapper _mapper;
        CrudGroupFlow workFlow;
        public CrudGroupController(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            workFlow = new CrudGroupFlow(uow);
        }


        [HttpGet(Name = "ListGroup_1")]
        public async Task<IActionResult> List(string sortName = "Id", string sortType = "ASC", int cursor = 0, int pageSize = 10)
        {
            Response response = workFlow.List();
            List<Group> items = (List<Group>)response.Result;
            CtrlUtil.ApplySort<Group, string>(ref items, sortName, sortType);
            ResponsePresenter res = CtrlUtil.ApplyPaging<Group, string>(cursor, pageSize, items);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpPost(Name = "CreateGroup_1")]
        public async Task<IActionResult> Create([FromBody] CreateGroupPresenter model)
        {
            Group group = _mapper.Map<Group>(model);
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
            Group group = _mapper.Map<Group>(model);
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
