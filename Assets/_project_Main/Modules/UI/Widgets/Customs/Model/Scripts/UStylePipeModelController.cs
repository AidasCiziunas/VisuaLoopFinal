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
    public class UStylePipeModelController : PipeModelController
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
                UStylePipeEdgeController uSPEC = (UStylePipeEdgeController)pEC;
                uSPEC.UpdateCollisionCoordFromOtherCollider(inCollision, _lastMovCoord, _colliMovCoord);
            }
            else if (pipeEdgeToUpdate == PipeEdgeType.C)
            {
                PipeEdgeController pEC = _pipeEdgeDataLs.Find(x => x.Type == PipeEdgeType.A).PipeEdgeController;
                UStylePipeEdgeController uSPEC = (UStylePipeEdgeController)pEC;
                uSPEC.UpdateCollisionCoordFromOtherCollider(inCollision, _lastMovCoord, _colliMovCoord);
            }
        }
    }
}
