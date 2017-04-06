using Sitecore.Pipelines;
using System.Web.Mvc;
using System.Web.Routing;

namespace Allwin.Sitecore.Modules.LiveList.Pipelines
{
    public class RegisterPublicRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("LiveList", "live-list/LiveList/LoadMore", new { controller = "LiveList", action = "LoadMore" });
        }
    }
}