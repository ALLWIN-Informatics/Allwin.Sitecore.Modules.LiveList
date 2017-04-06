using Allwin.Sitecore.Modules.LiveList.Models;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;

namespace Allwin.Sitecore.Modules.LiveList.Repositories
{
    /// <summary>
    /// LiveList repository
    /// </summary>
    public interface ILiveListRepository
    {
        /// <summary>
        /// Gets the LiveList items
        /// </summary>
        /// <returns>A single live list item</returns>
        LiveListItem GetLiveListItem(Item contextItem, string language);

        /// <summary>
        /// Gets the LiveList item container item
        /// </summary>
        /// <returns>The container object for the live list items</returns>
        LiveListItemContainer GetLiveListItemContainer(Item contextItem, string language);

        /// <summary>
        /// Gets more item to containeritem
        /// </summary>
        /// <param name="parentGuid">The id of the parent item</param>
        /// <param name="lastItemGuid">The last item's id from the list</param>
        /// <param name="numberOfItems">The number of items to be downloaded at once</param>
        /// <returns>A list of live list items to add ot the list</returns>
        IEnumerable<LiveListItem> GetNextSomeItems(Guid parentGuid, Guid lastItemGuid, int numberOfItems, string language);
    }
}