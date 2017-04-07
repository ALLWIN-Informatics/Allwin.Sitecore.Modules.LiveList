using System.Collections.Generic;
using Allwin.Sitecore.Modules.LiveList.Models.Field;
using Sitecore.Data;
using Sitecore.Globalization;
using Allwin.Sitecore.Modules.LiveList.Consts;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore;

namespace Allwin.Sitecore.Modules.LiveList.Services
{
    public class LiveListFieldService : ILiveListFieldService
    {
        private readonly Database _database;

        public LiveListFieldService(Database database)
        {
            _database = database;
        }

        public IEnumerable<Option> GetRenderingDefinitions(ID id, Language language)
        {
            var renderingDefs = new List<Option>();
            var contextItem = _database.GetItem(id, language);

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

            return renderingDefs;
        }

        public IEnumerable<Option> GetDatasources(Language language, string renderingDefId)
        {
            var datasources = new List<Option>();
            ID rDefId;
            if (!ID.TryParse(renderingDefId, out rDefId))
            {
                return datasources;
            }

            var renderingDefItem = _database.GetItem(rDefId, language);
            if (renderingDefItem == null)
            {
                return datasources;
            }

            MultilistField allowedTemplates = renderingDefItem.Fields[Templates.LiveListItemRendering.Fields.AllowedTemplates];
            if (allowedTemplates == null || allowedTemplates.Count == 0)
            {
                return datasources;
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

            return datasources;
        }
    }
}