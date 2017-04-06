using Allwin.Sitecore.Modules.LiveList.Hubs;
using Allwin.Sitecore.Modules.LiveList.Repositories;
using Sitecore.Data.Items;
using Microsoft.AspNet.SignalR;

namespace Allwin.Sitecore.Modules.LiveList.Services
{
    public class LiveListService : ILiveListService
    {
        private ILiveListRepository _liveListRepository { get; set; }

        public LiveListService()
        {
            _liveListRepository = new LiveListRepository();
        }

        public void CreateLiveListHtml(Item contextItem, string language)
        {
            var liveListItem = _liveListRepository.GetLiveListItem(contextItem, language);

            if (liveListItem == null)
            {
                return;
            }

            NotifyClients(liveListItem.Guid, liveListItem.ParentGuid, liveListItem.Html);
        }

        /// <summary>
        /// SignalR - notifies, and sends the new LiveList item
        /// </summary>
        /// <param name="id">The ID of the currently added item</param>
        /// <param name="parentId">The ID of the container item of the currently added item</param>
        /// <param name="itemHtml">The rendered html of the currently added item</param>
        private void NotifyClients(string id, string parentId, string itemHtml)
        {
            GlobalHost.ConnectionManager
                .GetHubContext<LiveListHub>()
                .Clients
                .Group(parentId)
                .refreshLiveList(parentId, id, itemHtml);
        }
    }
}