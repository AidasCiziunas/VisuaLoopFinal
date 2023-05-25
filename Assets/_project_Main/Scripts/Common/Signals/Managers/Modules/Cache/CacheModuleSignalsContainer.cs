using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main
{
    public class CacheSignal
    {
        public class Setup : CacheModuleASignal<Task<(Exception, bool)>> { }
    }

    public class Tex2dCacheSignal
    {
        public class Setup : CacheModuleASignal<Task<(Exception, bool)>> { }
        public class GetTex2d : CacheModuleASignal<int, int, TextureFormat, bool, Texture2D> { }
        public class ReleaseAllTex2d : CacheModuleASignal { }
    }

    public class RenderTexCacheSignal
    {
        public class Setup : CacheModuleASignal<Task<(Exception, bool)>> { }
        public class GetRenderTex : CacheModuleASignal<int, int, int, RenderTexture> { }
        public class ReleaseAllRenderTex : CacheModuleASignal { }
    }
}
