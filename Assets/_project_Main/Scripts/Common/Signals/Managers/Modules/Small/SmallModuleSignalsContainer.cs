using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main
{
    public class SmallSignal
    {
        public class Setup : SmallModuleASignal<Task<(Exception, bool)>> { }
    }

    public class DebuggingSignal
    {
        public class Setup : SmallModuleASignal<Task<(Exception, bool)>> { }
        public class Log : SmallModuleASignal<string, Void> { }
        public class ColorLog : SmallModuleASignal<string, DebugColor, Void> { }
        public class ColorLogObj : SmallModuleASignal<object, DebugColor, Void> { }
        public class LogStack : SmallModuleASignal<string, object, Void> { }
        public class LogError : SmallModuleASignal<string, Void> { }
        public class LogErrorStack : SmallModuleASignal<string, object, Void> { }
    }

    public class RectUISignal
    {
        public class Setup : SmallModuleASignal<Task<(Exception, bool)>> { }
        public class AdjustAnchor : SmallModuleASignal<RectTransform, AnchorPos, int, int, Void> { }
        public class AdjustPivot : SmallModuleASignal<RectTransform, PivotPos, Void> { }
    }

    public class StringSignal
    {
        public class Setup : SmallModuleASignal<Task<(Exception, bool)>> { }
        public class PutCharacterBwString : SmallModuleASignal<string, int, string, string> { }
        public class TrimString : SmallModuleASignal<string, int, string> { }
    }

    public class TextureSignal
    {
        public class Setup : SmallModuleASignal<Task<(Exception, bool)>> { }
        public class ConvertTextureToTexture2D : SmallModuleASignal<Texture, Texture2D> { }
    }
}
