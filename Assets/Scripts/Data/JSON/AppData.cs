using System;

namespace Data.JSON
{
    [Serializable]
    public class AppData
    {
        public string lastLang;
        public string lastStyle;

        public AppData() { }

        public AppData(string lang, string style)
        {
            lastLang = lang;
            lastStyle = style;
        }
    }
}