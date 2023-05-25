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
    public class TriLoopPipeModelController : PipeModelController
    {
        public override void Setup(PipeModelControllerProperties props)
        {
            _props = props;
        }

        public void UpdateCollisionDataToOtherColliderEdgeCtrl(PipeEdgeType pipeEdgeToUpdate, bool inCollision, Vector2 _lastMovCoord, Vector2 _colliMovCoord)
        {
            if (pipeEdgeToUpdate == PipeEdgeType.A)
            {
                PipeEdgeController pEC = _pipeEdgeDataLs.Find(x => x.Type == PipeEdgeType.C).PipeEdgeController;
                TriLoopPipeEdgeController tLPEC = (TriLoopPipeEdgeController)pEC;
                tLPEC.UpdateCollisionCoordFromOtherCollider(inCollision, _lastMovCoord, _colliMovCoord);
            }
            else if (pipeEdgeToUpdate == PipeEdgeType.C)
            {
                PipeEdgeController pEC = _pipeEdgeDataLs.Find(x => x.Type == PipeEdgeType.A).PipeEdgeController;
                TriLoopPipeEdgeController tLPEC = (TriLoopPipeEdgeController)pEC;
                tLPEC.UpdateCollisionCoordFromOtherCollider(inCollision, _lastMovCoord, _colliMovCoord);
            }
        }

        public void UpdateInitPosOfEdge(PipeEdgeType pipeEdgeType)
        {
            _pipeEdgeDataLs.Find(x => x.Type == pipeEdgeType).PipeEdgeController.EdgeTUpdater.UpdateInitPos();
        }

        public void ApplyRotationOnOtherEdgesAndHose(PipeEdgeType interactedEdge, PipeEdgeType edgeToRotate, PipeHoseType hose, float angle, bool touchDirection) // true for X, false for Y
        {
            MegaHose megaHose = _pipeHoseDataLs.Find(x => x.Type == hose).Hose;

            if (AxisType == AxisType.ZPos || AxisType == AxisType.ZNeg)
            {
                ModelAxisXMovement(interactedEdge, megaHose, angle);
            }
            else if (AxisType == AxisType.YPos || AxisType == AxisType.YNeg)
            {
                if (touchDirection)
                    ModelAxisXMovement(interactedEdge, megaHose, angle);
            }
        }

        private void ModelAxisXMovement(PipeEdgeType interactedEdge, MegaHose megaHose, float angle)
        {
            if (interactedEdge == PipeEdgeType.C)
            {
                megaHose.rotate = new Vector3(megaHose.rotate.x, megaHose.rotate.y - (angle * -1), megaHose.rotate.z);
            }
        }
    }
}
