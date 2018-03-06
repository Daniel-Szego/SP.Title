using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.Title
{
    public static class Constants
    {
        public static string AssetString = "Assets";
        public static string SettingsListName = "Titlewebpartpropertylist";
        public static string SettingsListPropertyName = "PropertyName";
        public static string SettingsListPropertyValue = "PropertyValue";
        public static string Webpartname = "SP.Title.webpart";

        public enum ImageStyle
        {
            None,
            Before,
            After,
            Continues
        };

    }

}
