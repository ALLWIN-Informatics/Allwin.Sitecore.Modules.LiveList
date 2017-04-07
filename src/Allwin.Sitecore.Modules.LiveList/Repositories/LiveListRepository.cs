using System;
using Allwin.Sitecore.Modules.LiveList.Models;
using Sitecore.Data.Items;
using System.Linq;
using Sitecore.Data.Fields;
using System.Collections.Generic;
using System.Net;
using Sitecore.Links;
using System.Text;
using Sitecore;
using Sitecore.Data;
using Allwin.Sitecore.Modules.LiveList.Consts;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using System.Web;
using System.Collections.Specialized;
using Sitecore.Text;

namespace Allwin.Sitecore.Modules.LiveList.Repositories
{
    public class LiveListRepository : ILiveListRepository
    {
        public LiveListItem GetLiveListItem(Item contextItem, string language)
        {
            if (contextItem == null)
            {
                return null;
            }

            var renderingId = contextItem[Templates.LiveListItem.Fields.Rendering];

            if (string.IsNullOrWhiteSpace(renderingId))
            {
                return null;
            }

            return new LiveListItem
            {
                Guid = contextItem.ID.Guid.ToString("N"),
                ParentGuid = contextItem.ParentID.Guid.ToString("N"),
                Html = RenderItem(contextItem, language)
            };
        }

        public LiveListItemContainer GetLiveListItemContainer(Item contextItem, string language)
        {
            if (contextItem == null)
            {
                return null;
            }

            int maxItemsOnPageLoad;
            if (!int.TryParse(contextItem[Templates.LiveListItemContainer.Fields.MaxItemsOnPageLoad], out maxItemsOnPageLoad))
            {
                maxItemsOnPageLoad = 10;
            }

            var settingsItem = contextItem.Database.GetItem(SitecoreIDs.LiveListSettings);

            return new LiveListItemContainer
            {
                Item = contextItem,
                Title = contextItem[Templates.LiveListItemContainer.Fields.Title],
                MaxItemsOnPageLoad = maxItemsOnPageLoad,
                Id = contextItem.ID.Guid.ToString("N"),
                Items = contextItem
                    .Children
                    .OrderByDescending(x => ((DateField)x.Fields[Templates.Updated]).DateTime)
                    .Take(maxItemsOnPageLoad)
                    .Select(c => GetLiveListItem(c, language))
                    .Where(x => x != null),
                ShowMoreText = contextItem[Templates.LiveListItemContainer.Fields.ShowMoreText],
                UseDefaultCss = UseDefaultCss(settingsItem)
            };
        }

        public IEnumerable<LiveListItem> GetNextSomeItems(Guid parentGuid, Guid lastItemGuid, int numberOfItems, string language)
        {
            var parentItem = Context.Database.GetItem(new ID(parentGuid));
            var lastItem = Context.Database.GetItem(new ID(lastItemGuid));
            var itemsList = parentItem.Children.OrderByDescending(x => ((DateField)x.Fields[Templates.Updated]).DateTime);
            var lastItemPlace = itemsList.ToList().FindIndex(x => x.ID == lastItem.ID) + 1;

            return itemsList.Skip(lastItemPlace).Take(numberOfItems).Select(c => GetLiveListItem(c, language)).Where(x => x != null);
        }

        /// <summary>
        /// Uses the default CSS or not.
        /// </summary>
        private bool UseDefaultCss(Item settingsItem)
        {            
            if (settingsItem == null)
            {
                return false;
            }

            CheckboxField useDefaultCssField = settingsItem.Fields[Templates.LiveListSettings.Fields.UseDefaultCss];
            return useDefaultCssField.Checked;
        }

        /// <summary>
        /// Gets the LiveList item html
        /// </summary>
        /// <returns>The rendered html of the context item</returns>
        private string RenderItem(Item contextItem, string language)
        {
            if (contextItem == null)
            {
                return string.Empty;
            }

            var urlOptions = new UrlOptions
            {
                EncodeNames = false,
                AlwaysIncludeServerUrl = true,
                LanguageEmbedding = LanguageEmbedding.Always,
                Language = Language.Parse(language)
            };

            var queryString = new UrlString(new NameValueCollection
            {
                {
                    "livelist", "true"
                }
            });
            

            // Respect master database edit, preview and normal mode
            if (contextItem.Database.Name == "master")
            {
                queryString.Add("sc_database", contextItem.Database.Name);
            }

            string html;
            var itemPath = string.Format("{0}?{1}" , LinkManager.GetItemUrl(contextItem, urlOptions) ?? string.Empty, queryString);
            
            using (var client = new WebClient())
            {
                try
                {
                    // Pass the .ASPXAUTH to the request to respect master database edit, preview and normal mode 
                    if (HttpContext.Current != null && HttpContext.Current.Request != null)
                    {
                        var authCookie = HttpContext.Current.Request.Cookies[Settings.AspxAuthCookieName];
                        if (authCookie != null)
                        {
                            var cookies = new UrlString(new NameValueCollection
                            {
                                {
                                    Settings.AspxAuthCookieName, authCookie.Value
                                }
                            });

                            client.Headers = new WebHeaderCollection();
                            client.Headers.Add(HttpRequestHeader.Cookie, cookies.ToString());
                        }
                    }
                    
                    html = client.DownloadString(itemPath);
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("[LiveList] Problem occured while get the HTML of {0}", itemPath), ex, this);
                    html = string.Empty;
                }
            }

            var bytes = Encoding.Default.GetBytes(html);
            html = Encoding.UTF8.GetString(bytes);

            return html;
        }
    }
}