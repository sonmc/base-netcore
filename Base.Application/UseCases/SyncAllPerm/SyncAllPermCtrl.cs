
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Base.Services.Base;
using Base.Services;

namespace Base.Application.UseCases
{
    [ApiController]
    [Route("api/sync-all-perm")]
    public class SyncAllPermController : ControllerBase
    {
        SyncAllPermFlow flow;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        public SyncAllPermController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            flow = new SyncAllPermFlow(new UnitOfWork());
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        [HttpGet(Name = "SyncAllPerm_")]
        public IActionResult SyncAllPerm()
        {
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items
               .Where(ad => ad.AttributeRouteInfo != null)
               .Select(x => new RouterPresenter
               {
                   Action = null != x && null != x.RouteValues && null != x.RouteValues["action"] ? x.RouteValues["action"] : "n/a",
                   Module = null != x && null != x.RouteValues && null != x.RouteValues["controller"] ? x.RouteValues["controller"] : "n/a",
                   Name = x.AttributeRouteInfo.Name ?? "n/a",
                   Template = x.AttributeRouteInfo.Template ?? "n/a",
                   Method = x.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.First()
               }).ToList();
            Response response = flow.SyncAllPerm(routes);

            return Ok(response);
        }
    }
}