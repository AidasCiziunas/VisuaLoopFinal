using Doozy.Runtime.Reactor.Animators;
using MegaFiers;
using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SyedAli.Main.UIModule.Widgets
{
    public class TriFlexPipeModelController : PipeModelController
    {
        public override void Setup(PipeModelControllerProperties props)
        {
            _props = props;
        }

        public override void ChangeParents(ParentChangeBeh pCB, PipeEdgeType edgeToBecomeChild, PipeHoseType hoseToBecomeChild, PipeEdgeType edgeToBecomeParent)
        {
            Transform edgeParentT = _pipeEdgeDataLs.Find(x => x.Type == edgeToBecomeParent)?.PipeEdgeController.EdgeCenPivT;

            Transform edgeChildT = _pipeEdgeDataLs.Find(x => x.Type == edgeToBecomeChild)?.PipeEdgeController.EdgeCenPivT;

            Transform hoseChildT = _pipeHoseDataLs.Find(x => x.Type == hoseToBecomeChild)?.HoseT;

            if (pCB == ParentChangeBeh.Do)
            {
                if (edgeParentT != null)
                {
                    if (edgeChildT != null)
                        edgeChildT.SetParent(edgeParentT);
                    if (hoseChildT != null)
                        hoseChildT.SetParent(edgeParentT);
                }
            }
            else if (pCB == ParentChangeBeh.Reset)
            {
                if (edgeChildT != null)
                    edgeChildT.SetParent(_edgeParentT);
                if (hoseChildT != null)
                    hoseChildT.SetParent(_edgeParentT);
            }
        }

        public void ApplyRotationOnOtherEdgesAndHose(PipeEdgeType interactedEdge, PipeEdgeType edgeToRotate, PipeHoseType hose, float angle, bool touchDirection) // true for X, false for Y
        {
            Transform edgeRotateCenT = _pipeEdgeDataLs.Find(x => x.Type == edgeToRotate)?.PipeEdgeController.EdgeCenPivT;
            MegaHose megaHose = _pipeHoseDataLs.Find(x => x.Type == hose).Hose;

            if (AxisType == AxisType.ZPos || AxisType == AxisType.ZNeg)
            {
                ModelAxisXMovement(edgeRotateCenT, interactedEdge, megaHose, angle);
            }
            else if (AxisType == AxisType.XPos || AxisType == AxisType.XNeg)
            {
                ModelAxisZMovement(edgeRotateCenT, angle);
            }
            else if (AxisType == AxisType.YPos || AxisType == AxisType.YNeg)
            {
                if (touchDirection)
                    ModelAxisXMovement(edgeRotateCenT, interactedEdge, megaHose, angle);
                else
                    ModelAxisZMovement(edgeRotateCenT, angle);
            }
        }

        private void ModelAxisXMovement(Transform edgeRotateCenT, PipeEdgeType interactedEdge, MegaHose megaHose, float angle)
        {
            if (edgeRotateCenT != null)
                edgeRotateCenT.localEulerAngles = new Vector3(edgeRotateCenT.localEulerAngles.x, edgeRotateCenT.localEulerAngles.y - angle, edgeRotateCenT.localEulerAngles.z);

            if (interactedEdge == PipeEdgeType.A)
            {
                megaHose.rotate = new Vector3(megaHose.rotate.x - (angle * -1), megaHose.rotate.y - (angle * -1), megaHose.rotate.z);
            }
            else if (interactedEdge == PipeEdgeType.D)
            {
                megaHose.rotate1 = new Vector3(megaHose.rotate1.x + angle, megaHose.rotate1.y + angle, megaHose.rotate1.z);
            }
        }

        private void ModelAxisZMovement(Transform edgeRotateCenT, float angle)
        {
            edgeRotateCenT.localEulerAngles = new Vector3(edgeRotateCenT.localEulerAngles.x + angle, edgeRotateCenT.localEulerAngles.y, edgeRotateCenT.localEulerAngles.z);
        }
    }
}
