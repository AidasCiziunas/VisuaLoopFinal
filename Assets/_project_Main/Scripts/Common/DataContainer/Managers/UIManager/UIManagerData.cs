namespace SyedAli.Main
{
    public enum UIFlow
    {
        None,
        Normal,
        Specific
    }

    // These names must match with the game object name on which window or panel controller script is placed
    public enum ScreenIds
    {
        None,
        SplashSimpleWindow,
        MainMenuSimpleWindow,

        InteractionToolsPanel,

        BasicPopUpWindow,
        InfoPopUpWindow,
        MenuPopUpWindow,

        SplashSimpleWindow_Mob,
        MainMenuSimpleWindow_Mob,

        InteractionToolsPanel_Mob,

        InfoPopUpWindow_Mob,
        MenuPopUpWindow_Mob

    }

    // Must match with the action name in New Controls (Input Actions) Window
    readonly struct UIInputAction
    {
        public const string Back = "Back";
    }


    public static class UIManagerDataConv
    {
        public static ScreenIds ScreenIdsStrToEnum(string str)
        {
            if (str == ScreenIds.None.ToString())
                return ScreenIds.None;
            else if (str == ScreenIds.SplashSimpleWindow.ToString())
                return ScreenIds.SplashSimpleWindow;
            else if (str == ScreenIds.MainMenuSimpleWindow.ToString())
                return ScreenIds.MainMenuSimpleWindow;

            else if (str == ScreenIds.InteractionToolsPanel.ToString())
                return ScreenIds.InteractionToolsPanel;

            else if (str == ScreenIds.BasicPopUpWindow.ToString())
                return ScreenIds.BasicPopUpWindow;
            else if (str == ScreenIds.InfoPopUpWindow.ToString())
                return ScreenIds.InfoPopUpWindow;
            else if (str == ScreenIds.MenuPopUpWindow.ToString())
                return ScreenIds.MenuPopUpWindow;


            else if (str == ScreenIds.SplashSimpleWindow_Mob.ToString())
                return ScreenIds.SplashSimpleWindow_Mob;
            else if (str == ScreenIds.MainMenuSimpleWindow_Mob.ToString())
                return ScreenIds.MainMenuSimpleWindow_Mob;

            else if (str == ScreenIds.InteractionToolsPanel_Mob.ToString())
                return ScreenIds.InteractionToolsPanel_Mob;

            else if (str == ScreenIds.InfoPopUpWindow_Mob.ToString())
                return ScreenIds.InfoPopUpWindow_Mob;
            else if (str == ScreenIds.MenuPopUpWindow_Mob.ToString())
                return ScreenIds.MenuPopUpWindow_Mob;

            else
            {
                SmallModuleSignals.Get<DebuggingSignal.LogError>().Dispatch("Unable to Find Relevant Enum against String for UIManagerData ScreenIds");
                return ScreenIds.None;
            }
        }
    }
}
