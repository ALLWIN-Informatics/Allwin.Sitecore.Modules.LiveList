using Allwin.Sitecore.Modules.LiveList.Models.Field;
using Sitecore.Data;
using Sitecore.Globalization;
using System.Collections.Generic;

namespace Allwin.Sitecore.Modules.LiveList.Services
{
    public interface ILiveListFieldService
    {
        /// <summary>
        /// Gets the rendering definitions.
        /// </summary>
        IEnumerable<Option> GetRenderingDefinitions(ID id, Language language);

        /// <summary>
        /// Gets the datasources.
        /// </summary>
        IEnumerable<Option> GetDatasources(Language language, string renderingDefId);
    }
}