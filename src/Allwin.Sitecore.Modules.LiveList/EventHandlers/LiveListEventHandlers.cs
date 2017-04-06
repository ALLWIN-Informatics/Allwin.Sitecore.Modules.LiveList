using System;
using Allwin.Sitecore.Modules.LiveList.Services;
using Sitecore.Data.Items;
using Sitecore.Events;
using System.Linq;
using Sitecore.Layouts;
using Sitecore.Data.Fields;
using Sitecore;
using Allwin.Sitecore.Modules.LiveList.Consts;
using Newtonsoft.Json;
using Allwin.Sitecore.Modules.LiveList.Models.Field;
using Sitecore.Data;

namespace Allwin.Sitecore.Modules.LiveList.EventHandlers
{
    public class LiveListEventHandlers
    {
        private readonly ILiveListService _liveListService;

        public LiveListEventHandlers() : base()
        {
            _liveListService = new LiveListService();
        }

        protected void OnItemSaving(object sender, EventArgs args)
        {
            var savedItem = Event.ExtractParameter(args, 0) as Item;

            if (savedItem != null && savedItem.Database.Name == "master" && savedItem.Template.BaseTemplates.Any(t => t.ID == Templates.LiveListItem.ID))
            {
                var layoutField = new LayoutField(savedItem);
                var layoutDefinition = LayoutDefinition.Parse(layoutField.Value);
                LayoutField.SetFieldValue(savedItem.Fields[FieldIDs.LayoutField], string.Empty);

                var sCompRendering = savedItem[Templates.LiveListItem.Fields.Rendering];
                if (string.IsNullOrWhiteSpace(sCompRendering))
                {
                    return;
                }

                var compRendering = JsonConvert.DeserializeObject<LiveListComplexRenderingField>(sCompRendering);
                if (compRendering == null || string.IsNullOrWhiteSpace(compRendering.RenderingDefinition))
                {
                    return;
                }

                var renderingDefItem = savedItem.Database.GetItem(ID.Parse(compRendering.RenderingDefinition));
                if (renderingDefItem == null)
                {
                    return;
                }

                var renderingDefinition = new RenderingDefinition
                {
                    Datasource = compRendering.Datasource,
                    Placeholder = Placeholders.LiveList,
                    ItemID = renderingDefItem[Templates.LiveListItemRendering.Fields.Rendering]
                };

                var deviceDefinition = layoutDefinition.GetDevice(SitecoreIDs.LiveListDevice.ToString());
                deviceDefinition.Layout = SitecoreIDs.LiveListLayout.ToString();

                if (deviceDefinition.Renderings != null)
                {
                    deviceDefinition.Renderings.Clear();
                }
                
                deviceDefinition.AddRendering(renderingDefinition);
                LayoutField.SetFieldValue(savedItem.Fields[FieldIDs.LayoutField], layoutDefinition.ToXml());
            }
        }

        protected void OnItemSaved(object sender, EventArgs args)
        {
            var savedItem = Event.ExtractParameter(args, 0) as Item;

            if (savedItem != null && savedItem.Database.Name == "web" && savedItem.Template.BaseTemplates.Any(t => t.ID == Templates.LiveListItem.ID))
            {
                _liveListService.CreateLiveListHtml(savedItem, savedItem.Language.ToString());
            }
        }
    }
}