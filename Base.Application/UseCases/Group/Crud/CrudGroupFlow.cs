using Base.Core.Schemas;
using Base.Utils;
using Base.Services;

namespace Base.Application.UseCases
{

    public class CrudGroupFlow
    {
        private readonly IUnitOfWork uow;
        public CrudGroupFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public Response List()
        {
            var groups = uow.Groups.FindAll();
            return new Response(Message.SUCCESS, groups);
        }

        public async Task<Response> Create(GroupSchema group)
        {
            var result = uow.Groups.Create(group);
            return new Response(Message.SUCCESS, result);
        }

        public async Task<Response> Update(GroupSchema group)
        {
            var result = uow.Groups.Update(group);
            return new Response(Message.SUCCESS, result);
        }

        public Response Deletes(int[] ids)
        {
            var result = uow.Users.Deletes(ids);
            return new Response(Message.SUCCESS, result);
        }

    }
}
