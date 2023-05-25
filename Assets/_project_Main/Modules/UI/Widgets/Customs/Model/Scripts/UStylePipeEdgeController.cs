using Doozy.Runtime.Reactor.Animators;
using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class UStylePipeEdgeController : PipeEdgeController
    {
        [SerializeField] private UStylePipeModelController _pipeCtrl;

        private bool _inCollision = false;
        private Vector2 _lastMovCoordNorm;
        private Vector2 _collideMovCoordNorm;

        private void OnTriggerEnter(Collider other)
        {
            _inCollision = true;
            _collideMovCoordNorm = _lastMovCoordNorm;

            if (_pipeCtrl.CurrentGrabbedEdge == _pipeEdgeType)
                _pipeCtrl.UpdateCollisionDataToOtherColliderEdgeCtrl(_pipeEdgeType, true, _lastMovCoordNorm * -1, _collideMovCoordNorm * -1);
        }

        private void OnTriggerExit(Collider other)
        {
            _inCollision = false;
            _collideMovCoordNorm = Vector3.zero;
        }

        public override void OnTouchDrag()
        {
            _pipeCtrl.CurrentGrabbedEdge = _pipeEdgeType;

            Vector2 coord = _touchControls.Touch.FirstFingerDeltaPosition.ReadValue<Vector2>();
            Vector2 origCoord = coord;

            coord = new Vector2(Mathf.Abs(coord.x), Mathf.Abs(coord.y));

            ApplyMovement(coord, origCoord, _pipeCtrl.Multiplier, _pipeCtrl.AxisType);
        }

        public override void OnTouchExit()
        {
            _pipeCtrl.CurrentGrabbedEdge = PipeEdgeType.None;

            _lockRotation = MeshMovementType.None;
        }

        public void UpdateCollisionCoordFromOtherCollider(bool inCollision, Vector2 _lastMov, Vector2 _coliMov)
        {
            _inCollision = inCollision;
            _lastMovCoordNorm = _lastMov;
            _collideMovCoordNorm = _coliMov;
        }

        private void ApplyMovement(Vector2 coord, Vector2 origCoord, float multi, AxisType axisType)
        {
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

            float rotationPer001 = 2.5f;

            if (axisType == AxisType.ZPos || axisType == AxisType.ZNeg)
            {
                if (coord.x > coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.X))
                {
                    if (ShouldAllowMovement(origCoord) == false)
                        return;

                    float val = axisType == AxisType.ZPos ? multi * origCoord.x : multi * -origCoord.x;

                    Vector3 p = new Vector3(xPiv + val, yPiv, zPiv);
                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        pivT.transform.localPosition = p;

                    float angle = CalculateAngle(val, rotationPer001);
                    //pivT.transform.localEulerAngles = new Vector3(pivT.localEulerAngles.x, pivT.localEulerAngles.y - angle, pivT.localEulerAngles.z);

                    _lastMovCoordNorm = origCoord.normalized;
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
                    if (ShouldAllowMovement(origCoord) == false)
                        return;

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

                    _lastMovCoordNorm = origCoord.normalized;
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
                    if (ShouldAllowMovement(origCoord) == false)
                        return;

                    float val = axisType == AxisType.YPos ? multi * origCoord.x : multi * origCoord.x;
                    Vector3 p = new Vector3(xPiv + val, yPiv, zPiv);

                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        pivT.transform.localPosition = p;

                    float angle = CalculateAngle(val, rotationPer001);
                    //pivT.transform.localEulerAngles = new Vector3(pivT.localEulerAngles.x, pivT.localEulerAngles.y - angle, pivT.localEulerAngles.z);

                    _lastMovCoordNorm = origCoord.normalized;
                    _lockRotation = MeshMovementType.X;
                }
                else if (coord.x < coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.Y))
                {
                    if (ShouldAllowMovement(origCoord) == false)
                        return;

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

                    _lastMovCoordNorm = origCoord.normalized;
                    _lockRotation = MeshMovementType.Y;
                }
            }
        }

        private bool ShouldAllowMovement(Vector2 origCoord)
        {
            if (_inCollision)
            {
                if (_collideMovCoordNorm.x < 0 && origCoord.x < 0)
                    return false;
                else if (_collideMovCoordNorm.x > 0 && origCoord.x > 0)
                    return false;
                else if (_collideMovCoordNorm.y < 0 && origCoord.y < 0)
                    return false;
                else if (_collideMovCoordNorm.y > 0 && origCoord.y > 0)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }
    }
}
