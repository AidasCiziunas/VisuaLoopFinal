using System;

namespace SyedAli.Main
{
    [Serializable]
    public class LinkedinButtonWidgetProperties
    {
        public readonly string URL;

        public LinkedinButtonWidgetProperties(string uRL)
        {
            URL = uRL;
        }
    }

    public class LinkedinBtnClickedEventArgs : EventArgs
    {
        public readonly string URL;

        public LinkedinBtnClickedEventArgs(string uRL)
        {
            URL = uRL;
        }
    }
}
