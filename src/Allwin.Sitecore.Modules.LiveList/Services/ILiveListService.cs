using Allwin.Sitecore.Modules.LiveList.Models.Field;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using System.Collections.Generic;

namespace Allwin.Sitecore.Modules.LiveList.Services
{
    public interface ILiveListService
    {
        /// <summary>
        /// Creates a LiveList Item
        /// </summary>
        void CreateLiveListHtml(Item contextItem, string language);
    }
}
