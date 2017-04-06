using Allwin.Sitecore.Modules.LiveList.Consts;
using Allwin.Sitecore.Modules.LiveList.Models.Field;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Allwin.Sitecore.Modules.LiveList.Controllers
{
    public class LiveListComplexRenderingFieldController : Controller
    {
        private readonly Database _database;

        public LiveListComplexRenderingFieldController()
        {
            _database = Factory.GetDatabase("master");
        }

        public ActionResult Render()
        {
            return View("~/Views/LiveList/ComplexRenderingField/Render.cshtml");
        }

        [HttpGet]
        public ActionResult GetRenderingDefinitions(string id, string la, string vs)
        {
            var renderingDefs = new List<Option>();
            var contextItem = _database.GetItem(ID.Parse(id), Language.Parse(la));

            var renderingDefContainer = contextItem.Axes.SelectSingleItem(string.Format("./ancestor::*[@@templateid='{0}']/*[@@templateid='{1}']",
                Templates.LiveListFolder.ID,
                Templates.LiveListRenderingContainer.ID));

            foreach (Item renderingDefItem in renderingDefContainer.Children)
            {
                renderingDefs.Add(new Option
                {
                    Text = renderingDefItem.Name,
                    Value = renderingDefItem.ID.ToString()
                });
            }
            
            return Json(new { RenderingDefs = renderingDefs }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDatasources(string id, string la, string vs, string renderingDefId)
        {
            var datasources = new List<Option>();
            ID rDefId;
            if (!ID.TryParse(renderingDefId, out rDefId))
            {
                return Json(new { Items = datasources }, JsonRequestBehavior.AllowGet);
            }

            var renderingDefItem = _database.GetItem(rDefId, Language.Parse(la));
            if (renderingDefItem == null)
            {
                return Json(new { Items = datasources }, JsonRequestBehavior.AllowGet);
            }

            MultilistField allowedTemplates = renderingDefItem.Fields[Templates.LiveListItemRendering.Fields.AllowedTemplates];
            if (allowedTemplates == null || allowedTemplates.Count == 0)
            {
                return Json(new { Items = datasources }, JsonRequestBehavior.AllowGet);
            }

            var datasourceContainer = renderingDefItem.Axes.SelectSingleItem(string.Format("./ancestor::*[@@templateid='{0}']/*[@@templateid='{1}']",
                Templates.LiveListFolder.ID,
                Templates.LiveListDatasourceContainer.ID));

            foreach (Item item in allowedTemplates.GetItems())
            {
                foreach (var reference in Globals.LinkDatabase.GetItemReferrers(item, false))
                {
                    var sourceItem = reference.GetSourceItem();
                    if (sourceItem.Paths.FullPath.Contains(datasourceContainer.Paths.FullPath))
                    {
                        datasources.Add(new Option
                        {
                            Text = sourceItem.Name,
                            Value = sourceItem.ID.ToString()
                        });
                    }
                }
            }

            return Json(new { Datasources = datasources }, JsonRequestBehavior.AllowGet);
        }
    }
}