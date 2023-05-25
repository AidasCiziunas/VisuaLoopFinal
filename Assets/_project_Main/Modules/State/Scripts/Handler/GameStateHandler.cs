using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SyedAli.Main.GameStateSignal;

namespace SyedAli.Main.StateModule
{
    internal class GameStateHandler : MonoBehaviour
    {
        private List<PipeStateData> _pipeInfoDataLs = new List<PipeStateData>();
        private PipeType _selectedPipeType;

        private void OnEnable()
        {
            StateModuleSignals.Get<GameStateSignal.Setup>().AddListener(OnSetup);
            StateModuleSignals.Get<GameStateSignal.UpdatePipeInfoData>().AddListener(OnUpdatePipeInfoData);
            StateModuleSignals.Get<GameStateSignal.UpdateSelectedPipeType>().AddListener(OnUpdateSelectedPipeType);
            StateModuleSignals.Get<GameStateSignal.GetPipeInfoData>().AddListener(OnGetPipeInfoData);
            StateModuleSignals.Get<GameStateSignal.GetSelectedPipeType>().AddListener(OnGetSelectedPipeType);
        }

        private void OnDisable()
        {
            StateModuleSignals.Get<GameStateSignal.Setup>().RemoveListener(OnSetup);
            StateModuleSignals.Get<GameStateSignal.UpdatePipeInfoData>().RemoveListener(OnUpdatePipeInfoData);
            StateModuleSignals.Get<GameStateSignal.UpdateSelectedPipeType>().RemoveListener(OnUpdateSelectedPipeType);
            StateModuleSignals.Get<GameStateSignal.GetPipeInfoData>().RemoveListener(OnGetPipeInfoData);
            StateModuleSignals.Get<GameStateSignal.GetSelectedPipeType>().RemoveListener(OnGetSelectedPipeType);
        }

        private async Task<(Exception, bool)> OnSetup()
        {
            SmallModuleSignals.Get<DebuggingSignal.Log>().Dispatch("GameStateHandler Initialization");
            return (null, true);
        }

        private Void OnUpdatePipeInfoData(PipeType pipeType, TextAsset txtAsset)
        {
            _pipeInfoDataLs.Add(new PipeStateData(pipeType, txtAsset));
            return null;
        }

        private Void OnUpdateSelectedPipeType(PipeType pipeType)
        {
            _selectedPipeType = pipeType;
            return null;
        }

        private TextAsset OnGetPipeInfoData(PipeType pipeType)
        {
            return _pipeInfoDataLs.Find(x => x.PipeType == pipeType).PipeInfoTxtAsst;
        }

        private PipeType OnGetSelectedPipeType()
        {
            return _selectedPipeType;
        }
    }
}
