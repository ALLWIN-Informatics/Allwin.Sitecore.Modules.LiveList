using Sitecore.Data;

namespace Allwin.Sitecore.Modules.LiveList.Consts
{
    public struct Templates
    {
        public static readonly ID Updated = new ID("{D9CF14B1-FA16-4BA6-9288-E8A174D4D522}");

        public struct LiveListItem
        {
            public static readonly ID ID = new ID("{967CFD6C-B296-4B04-A130-9FE029AA5FCA}");

            public struct Fields
            { 
                public static readonly ID Rendering = new ID("{C2E4019D-47A0-49B3-9356-D043EE3FC5A5}");
            }
        }

        public struct LiveListItemContainer
        {
            public static readonly ID ID = new ID("{6368D6FE-5ADD-46E8-BD6E-DF97E6324EF9}");

            public struct Fields
            {
                public static readonly ID Title = new ID("{C5F57376-30EC-4BDA-AF84-33F84B6B79E8}");

                public static readonly ID MaxItemsOnPageLoad = new ID("{63ED60AB-A1CB-4B31-A34F-8AEB3ADAF39F}");

                public static readonly ID ShowMoreText = new ID("{CD59B66E-F158-48F3-B9D0-5A6063D5EE00}");
            }

            public struct FieldNames
            {
                public static readonly string Title = "Title";

                public static readonly string ShowMoreText = "Show more text";
            }
        }

        public struct LiveListItemRendering
        {
            public struct Fields
            {
                public static readonly ID Rendering = new ID("{2CEE9233-DD19-41D4-9C60-30C36256F96A}");

                public static readonly ID AllowedTemplates = new ID("{15354ED9-07BF-45D3-BB5E-18CBAACB2DF1}");
            }
        }

        public struct LiveListFolder
        {
            public static readonly ID ID = new ID("{14C6E3D7-B08C-4E89-80D3-A342CF24059D}");
        }

        public struct LiveListRenderingContainer
        {
            public static readonly ID ID = new ID("{B0CD0D5C-AE6B-4A47-878A-974AE71BE26A}");
        }

        public struct LiveListDatasourceContainer
        {
            public static readonly ID ID = new ID("{B4506005-AFDC-4F1A-BF6D-00252D007D07}");
        }

        public struct LiveListSettings
        {
            public struct Fields
            {
                public static readonly ID UseDefaultCss = new ID("{316FB6B4-966F-4942-BCFB-8BF66663D95F}");
            }
        }
    }
}