using Doozy.Runtime.Reactor.Animators;
using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class VStylePipeEdgeController : PipeEdgeController
    {
        [SerializeField] private VStylePipeModelController _pipeCtrl;

        public override void OnTouchDrag()
        {
            Vector2 coord = _touchControls.Touch.FirstFingerDeltaPosition.ReadValue<Vector2>();
            Vector2 origCoord = coord;

            coord = new Vector2(Mathf.Abs(coord.x), Mathf.Abs(coord.y));

            ApplyMovement(coord, origCoord, _pipeCtrl.Multiplier, _pipeCtrl.AxisType);
        }

        public override void OnTouchExit()
        {
            _lockRotation = MeshMovementType.None;
        }



        private void ApplyMovement(Vector2 coord, Vector2 origCoord, float multi, AxisType axisType)
        {        
            float divider = 3;
            float rotationPer001 = 3.7f;

            Transform pivAT = _edgePivotDataLs.Find(x => x.Type == EdgePivotType.A)?.EdgePivotT;
            Transform pivBT = _edgePivotDataLs.Find(x => x.Type == EdgePivotType.B)?.EdgePivotT;

            Transform pivT = null;
            if (_pipeEdgeType == PipeEdgeType.A)
                pivT = pivBT;
            else if (_pipeEdgeType == PipeEdgeType.C)
                pivT = pivAT;

            float xPiv = pivT.localPosition.x;
            float yPiv = pivT.localPosition.y;
            float zPiv = pivT.localPosition.z;


            if (axisType == AxisType.ZPos || axisType == AxisType.ZNeg)
            {
                if (coord.x > coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.X))
                {
                    float val = axisType == AxisType.ZPos ? multi * origCoord.x : multi * -origCoord.x;

                    float zVal = 0.0f;
                    if (_pipeEdgeType == PipeEdgeType.A)
                        zVal = coord.x > 0 ? zPiv + val / divider : zPiv - val / divider;
                    else if (_pipeEdgeType == PipeEdgeType.C)
                        zVal = coord.x > 0 ? zPiv - val / divider : zPiv + val / divider;

                    Vector3 p = new Vector3(xPiv + val, yPiv, zVal);
                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        pivT.transform.localPosition = p;

                    float angle = CalculateAngle(val, rotationPer001);
                    //pivT.transform.localEulerAngles = new Vector3(pivT.localEulerAngles.x, pivT.localEulerAngles.y - angle, pivT.localEulerAngles.z);

                    _lockRotation = MeshMovementType.X;
                }
                else if (coord.x < coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.Z))
                {
                }
            }
            else if (axisType == AxisType.XPos || axisType == AxisType.XNeg)
            {
                if (coord.x > coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.Y))
                {
                    float val = axisType == AxisType.XPos ? multi * origCoord.x : multi * -origCoord.x;
                    Vector3 p = new Vector3(xPiv, yPiv + val, zPiv);

                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        pivT.transform.localPosition = p;

                    float angle = CalculateAngle(val, rotationPer001);
                    //pivT.transform.localEulerAngles = new Vector3(pivT.localEulerAngles.x + angle, pivT.localEulerAngles.y, pivT.localEulerAngles.z);

                    _lockRotation = MeshMovementType.Y;
                }
                else if (coord.x < coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.Z))
                {
                }
            }
            else if (axisType == AxisType.YPos || axisType == AxisType.YNeg)
            {
                if (coord.x > coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.X))
                {
                    float val = axisType == AxisType.YPos ? multi * origCoord.x : multi * origCoord.x;

                    float zVal = 0.0f;
                    if (_pipeEdgeType == PipeEdgeType.A)
                        zVal = coord.x > 0 ? zPiv + val / divider : zPiv - val / divider;
                    else if (_pipeEdgeType == PipeEdgeType.C)
                        zVal = coord.x > 0 ? zPiv - val / divider : zPiv + val / divider;

                    Vector3 p = new Vector3(xPiv + val, yPiv, zVal);

                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        pivT.transform.localPosition = p;

                    float angle = CalculateAngle(val, rotationPer001);
                    //pivT.transform.localEulerAngles = new Vector3(pivT.localEulerAngles.x, pivT.localEulerAngles.y - angle, pivT.localEulerAngles.z);

                    _lockRotation = MeshMovementType.X;
                }
                else if (coord.x < coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.Y))
                {
                    float val = axisType == AxisType.YPos ? multi * origCoord.y : multi * -origCoord.y;
                    Vector3 p = new Vector3(xPiv, yPiv + val, zPiv);

                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        pivT.transform.localPosition = p;

                    float angle = CalculateAngle(val, rotationPer001);
                    //pivT.transform.localEulerAngles = new Vector3(pivT.localEulerAngles.x + angle, pivT.localEulerAngles.y, pivT.localEulerAngles.z);

                    _lockRotation = MeshMovementType.Y;
                }
            }
        }
    }
}
