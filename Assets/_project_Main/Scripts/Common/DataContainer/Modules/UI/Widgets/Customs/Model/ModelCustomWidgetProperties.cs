using MegaFiers;
using SyedAli.Main.UIModule.Widgets;
using System;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace SyedAli.Main
{
    public enum ModelWidgetMode
    {
        ShowOnly, ThreeD, Move, Zoom, Info, Visibility
    }

    public enum InstantChangeAxis
    {
        None, InstXAxis, InstYAxis, InstZAxis
    }

    public enum ModelRotationType
    {
        None, XPos, XNeg, YPos, YNeg, ZClock, ZAntiClock
    }

    public enum AxisType
    {
        None, XPos, XNeg, YPos, YNeg, ZPos, ZNeg
    }

    public enum MeshMovementType
    {
        None, X, Y, Z
    }

    public enum PipeEdgeType
    {
        None, A, B, C, D
    }
    
    public enum EdgePivotType
    {
        None, A, B
    }

    public enum PipeHoseType
    {
        None, AB, BC, CD
    }

    public enum TouchState { TOUCH, NONE };

    public enum ParentChangeBeh
    {
        DoNothing, Do, Reset
    }

    public enum ResetType
    {
        ThreeD, Move, Zoom
    }

    public enum ZoomType
    {
        PinchIn, PinchOut
    }

    [Serializable]
    public class PipeEdgeData
    {
        public PipeEdgeType Type;
        public PipeEdgeController PipeEdgeController;

        public PipeEdgeData(PipeEdgeType type, PipeEdgeController pipeEdgeController)
        {
            Type = type;
            PipeEdgeController = pipeEdgeController;
        }
    }

    [Serializable]
    public class EdgePivotData
    {
        public EdgePivotType Type;
        public Transform EdgePivotT;

        public EdgePivotData(EdgePivotType type, Transform edgePivotT)
        {
            Type = type;
            EdgePivotT = edgePivotT;
        }
    }

    [Serializable]
    public class PipeHoseData
    {
        public PipeHoseType Type;
        public MegaHose Hose;
        public Transform HoseT;
        public HoseResetter HoseResetter;

        public PipeHoseData(PipeHoseType type, MegaHose hose, Transform hoseT, HoseResetter hoseResetter)
        {
            Type = type;
            Hose = hose;
            HoseT = hoseT;
            HoseResetter = hoseResetter;
        }
    }

    [Serializable]
    public class ModelCustomWidgetProperties
    {
        public readonly ModelWidgetMode ModelWidMode;
        public readonly InstantChangeAxis InstantChangeAxis;
        public GameObject ModelGo;
        public float Zoom;
        public bool InstantChangeAxisDirection;
        public bool Visibility;

        public ModelCustomWidgetProperties(ModelWidgetMode modelWidMode, InstantChangeAxis instantChangeAxis, GameObject modelGo, float zoom, bool instantChangeAxisDirection, bool visibility = true)
        {
            ModelWidMode = modelWidMode;
            InstantChangeAxis = instantChangeAxis;
            ModelGo = modelGo;
            Zoom = zoom;
            InstantChangeAxisDirection = instantChangeAxisDirection;
            Visibility = visibility;
        }
    }

    [SerializeField]
    public class PipeModelControllerProperties
    {
        public readonly float Multiplier;
        public readonly AxisType ModelCurrentAxisType;

        public PipeModelControllerProperties(float multiplier, AxisType modelCurrentAxisType)
        {
            Multiplier = multiplier;
            ModelCurrentAxisType = modelCurrentAxisType;
            //Debug.Log("Current Axis = " + ModelCurrentAxisType);
        }
    }

    public class ModelMoveBehaviourEventArgs : EventArgs
    {
        public readonly bool BehaviourStatus;

        public ModelMoveBehaviourEventArgs(bool behaviourStatus)
        {
            BehaviourStatus = behaviourStatus;
        }
    }

    public class ModelZoomBehaviourEventArgs : EventArgs
    {
        public readonly ZoomType ZoomType;

        public ModelZoomBehaviourEventArgs(ZoomType zoomType)
        {
            ZoomType = zoomType;
        }
    }
}
