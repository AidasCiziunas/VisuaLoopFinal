using System;

namespace SyedAli.Main
{
    [Serializable]
    public class FacebookButtonWidgetProperties
    {
        public readonly string URL;

        public FacebookButtonWidgetProperties(string uRL)
        {
            URL = uRL;
        }
    }

    public class FacebookBtnClickedEventArgs : EventArgs
    {
        public readonly string URL;

        public FacebookBtnClickedEventArgs(string uRL)
        {
            URL = uRL;
        }
    }
}
