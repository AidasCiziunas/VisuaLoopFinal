namespace SyedAli.Main
{
    #region Windows
    public class SplashSimpleWindowSignal
    {
        public class ChangeToNewScreen : UIModuleASignal<ScreenIds, WindowProperties, ScreenIds, PanelProperties, ScreenIds, Void> { }
    }
    public class MainMenuSimpleWindowSignal
    {
        public class ChangeToNewScreen : UIModuleASignal<ScreenIds, WindowProperties, ScreenIds, PanelProperties, ScreenIds, Void> { }
        public class DrawerInteraction : UIModuleASignal<DrawerBehaviourType, float, Void> { }
        public class GetSelectedPipeType : UIModuleASignal<PipeType> { }
    }
    #endregion Windows

    #region Panels
    public class InteractionToolsPanelSignal
    {
        public class ChangeToNewScreen : UIModuleASignal<ScreenIds, WindowProperties, ScreenIds, PanelProperties, ScreenIds, Void> { }
        public class PanelInteractionClicked : UIModuleASignal<InteractionType, Void> { }
    }
    public class MenuPopUpWindowSignal
    {
        public class ChangeToNewScreen : UIModuleASignal<ScreenIds, WindowProperties, ScreenIds, PanelProperties, ScreenIds, Void> { }
    }
    #endregion Panels

    #region PopUps
    public class BasicPopUpWindowSignal
    {
        public class ChangeToNewScreen : UIModuleASignal<ScreenIds, WindowProperties, ScreenIds, PanelProperties, ScreenIds, Void> { }
    }
    public class InfoPopUpWindowSignal
    {
        public class ChangeToNewScreen : UIModuleASignal<ScreenIds, WindowProperties, ScreenIds, PanelProperties, ScreenIds, Void> { }
        public class InfoPopUpClosed : UIModuleASignal { }
    }
    #endregion PopUps
}
