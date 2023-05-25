using System;
using System.Collections.Generic;
using UnityEngine;

namespace SyedAli.Main
{
    public enum MainMenuMode
    {
        Full,
        Small
    }

    public enum PipeType
    {
        VStyleLoop, UStyleLoop, TriLoop, TriFlexLoop
    }

    public enum DrawerBehaviourType
    {
        Opened,
        Closed
    }

    [Serializable]
    public struct PipeData
    {
        public PipeType PipeType;
        public string PipeName;
        public Sprite PipeSprt;
        public GameObject PipePrfb;
        public TextAsset PipeInfoTxtAst;
    }

    [Serializable]
    public struct InstantiatedPipeData
    {
        public PipeType PipeType;
        public GameObject PipeGo;

        public InstantiatedPipeData(PipeType pipeType, GameObject pipeGo)
        {
            PipeType = pipeType;
            PipeGo = pipeGo;
        }
    }

    [Serializable]
    public class MainMenuSimpleWindowProperties : WindowProperties
    {
    }
}
