using Allwin.Sitecore.Modules.LiveList.Services;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Globalization;
using System.Web.Mvc;

namespace Allwin.Sitecore.Modules.LiveList.Controllers
{
    public class LiveListComplexRenderingFieldController : Controller
    {
        private readonly ILiveListFieldService _liveListFieldService;

        public LiveListComplexRenderingFieldController()
        {
            _liveListFieldService = new LiveListFieldService(Factory.GetDatabase("master"));
        }

        public ActionResult Render()
        {
            return View("~/Views/LiveList/ComplexRenderingField/Render.cshtml");
        }

        [HttpGet]
        public ActionResult GetRenderingDefinitions(string id, string la, string vs)
        {            
            return Json(new
            {
                RenderingDefs = _liveListFieldService.GetRenderingDefinitions(ID.Parse(id), Language.Parse(la))
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDatasources(string id, string la, string vs, string renderingDefId)
        {
            return Json(new
            {
                Datasources = _liveListFieldService.GetDatasources(Language.Parse(la), renderingDefId)
            },
            JsonRequestBehavior.AllowGet);
        }
    }
}