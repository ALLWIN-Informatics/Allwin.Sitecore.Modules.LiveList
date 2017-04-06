using Sitecore.Data.Items;

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
