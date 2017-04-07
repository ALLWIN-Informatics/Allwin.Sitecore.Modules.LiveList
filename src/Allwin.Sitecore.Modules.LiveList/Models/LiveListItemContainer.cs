using Sitecore.Data.Items;
using System.Collections.Generic;

namespace Allwin.Sitecore.Modules.LiveList.Models
{
    /// <summary>
    /// LiveList item container model
    /// </summary>
    public class LiveListItemContainer
    {
        public Item Item { get; set; }

        public string Title { get; set; }

        public int MaxItemsOnPageLoad { get; set; }

        public IEnumerable<LiveListItem> Items { get; set; }

        public string Id { get; set; }

        public string ShowMoreText { get; set; }

        public bool UseDefaultCss { get; set; }
    }
}