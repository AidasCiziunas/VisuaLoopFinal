using System;
using System.Threading.Tasks;

namespace SyedAli.Main
{
    public class AppManagerSignal
    {
        public class AllModulesInitialized : ManagersASignal { }
        public class GetTargetDevice : ManagersASignal<TargetResolution> { }
    }

    public class UIManagerSignal
    {
        public class Setup : ManagersASignal<BuildType, TargetResolution, bool, Task<(Exception, bool)>> { }
        public class ChangeToNewScreen : ManagersASignal<ScreenIds, WindowProperties, ScreenIds, PanelProperties, ScreenIds, Void> { }
        public class IsWindowOnTopOfAllWindows : ManagersASignal<IWindowController, bool> { }
        public class GetCurrentTopWindow : ManagersASignal<ScreenIds> { }
        public class IsThisWindowOpened : ManagersASignal<ScreenIds, bool> { }
        public class HideAllScreens : ManagersASignal { }
    }
}
