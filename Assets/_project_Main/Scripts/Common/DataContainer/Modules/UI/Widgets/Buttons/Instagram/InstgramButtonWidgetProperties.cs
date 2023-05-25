using System;

namespace SyedAli.Main
{
    [Serializable]
    public class InstagramButtonWidgetProperties
    {
        public readonly string URL;

        public InstagramButtonWidgetProperties(string uRL)
        {
            URL = uRL;
        }
    }

    public class InstagramBtnClickedEventArgs : EventArgs
    {
        public readonly string URL;

        public InstagramBtnClickedEventArgs(string uRL)
        {
            URL = uRL;
        }
    }
}
