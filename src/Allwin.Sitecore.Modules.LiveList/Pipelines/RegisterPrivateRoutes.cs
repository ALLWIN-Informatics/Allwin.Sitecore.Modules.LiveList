using Sitecore.Pipelines;
using System.Web.Mvc;
using System.Web.Routing;

namespace Allwin.Sitecore.Modules.LiveList.Pipelines
{
    public class RegisterPrivateRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("LiveListFieldRender", "live-list/LiveListComplexRenderingField/Render", new { controller = "LiveListComplexRenderingField", action = "Render" });
            RouteTable.Routes.MapRoute("LiveListFieldRenderingDefinitions", "live-list/LiveListComplexRenderingField/GetRenderingDefinitions", new { controller = "LiveListComplexRenderingField", action = "GetRenderingDefinitions" });
            RouteTable.Routes.MapRoute("LiveListFieldDatasources", "live-list/LiveListComplexRenderingField/GetDatasources", new { controller = "LiveListComplexRenderingField", action = "GetDatasources" });
        }
    }
}