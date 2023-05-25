using System;

namespace SyedAli.Main
{
    [Serializable]
    public class WebsiteButtonWidgetProperties
    {
        public readonly string URL;

        public WebsiteButtonWidgetProperties(string uRL)
        {
            URL = uRL;
        }
    }

    public class WebsiteBtnClickedEventArgs : EventArgs
    {
        public readonly string URL;

        public WebsiteBtnClickedEventArgs(string uRL)
        {
            URL = uRL;
        }
    }
}
