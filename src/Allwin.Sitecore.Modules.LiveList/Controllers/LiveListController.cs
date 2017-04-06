using Allwin.Sitecore.Modules.LiveList.Repositories;
using Sitecore;
using Sitecore.Data;
using Sitecore.Mvc.Presentation;
using System;
using System.Web.Mvc;

namespace Allwin.Sitecore.Modules.LiveList.Controllers
{
    public class LiveListController : Controller
    {
        private readonly ILiveListRepository _liveListRepository;

        public LiveListController()
        {
            _liveListRepository = new LiveListRepository();
        }

        /// <summary>
        /// Livelist module rendering controller
        /// </summary>
        /// <returns>The live list view</returns>
        public ActionResult LiveList()
        {
            var datasourceItem = Context.Database.GetItem(RenderingContext.Current.Rendering.DataSource);
            var model = _liveListRepository.GetLiveListItemContainer(datasourceItem, Context.Language.ToString());

            if (model == null)
            {
                return new EmptyResult();
            }

            return View("~/Views/LiveList/LiveList.cshtml", model);
        }

        /// <summary>
        /// Livelist more item loader controller
        /// </summary>
        /// <param name="parentId">The parent id</param>
        /// <param name="lastId">The id of the last item in the list</param>
        /// <param name="numberOfItems">The number of items to download at once</param>
        /// <returns>The short version of the live list view</returns>
        public ActionResult LoadMore(Guid parentId, Guid lastId, int numberOfItems, string language)
        {
            var lastItem = Context.Database.GetItem(new ID(lastId));
            var model = _liveListRepository.GetNextSomeItems(parentId, lastId, numberOfItems, language);
            return View("~/Views/LiveList/LiveListShort.cshtml", model);
        }
    }
}