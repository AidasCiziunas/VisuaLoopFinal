using System;
using System.Collections.Generic;
using UnityEngine;

namespace SyedAli.Main
{
    [Serializable]
    public class PipeStateData
    {
        public PipeType PipeType;
        public TextAsset PipeInfoTxtAsst;

        public PipeStateData(PipeType pipeType, TextAsset pipeInfoTxtAsst)
        {
            PipeType = pipeType;
            PipeInfoTxtAsst = pipeInfoTxtAsst;
        }
    }
}
