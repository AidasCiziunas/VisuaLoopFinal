using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main
{
    public class StateSignal
    {
        public class Setup : StateModuleASignal<Task<(Exception, bool)>> { }
    }

    public class GameStateSignal
    {
        public class Setup : StateModuleASignal<Task<(Exception, bool)>> { }

        public class UpdatePipeInfoData : StateModuleASignal<PipeType, TextAsset, Void> { }
        public class UpdateSelectedPipeType : StateModuleASignal<PipeType, Void> { }

        public class GetPipeInfoData : StateModuleASignal<PipeType, TextAsset> { }
        public class GetSelectedPipeType : StateModuleASignal<PipeType> { }
    }
}
