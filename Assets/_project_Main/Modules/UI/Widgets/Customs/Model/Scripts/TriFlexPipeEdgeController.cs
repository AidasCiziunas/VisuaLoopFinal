using Doozy.Runtime.Reactor.Animators;
using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class TriFlexPipeEdgeController : PipeEdgeController
    {
        [SerializeField] private TriFlexPipeModelController _pipeCtrl;

        public override void OnTouchDrag()
        {
            Vector2 coord = _touchControls.Touch.FirstFingerDeltaPosition.ReadValue<Vector2>();
            Vector2 origCoord = coord;

            coord = new Vector2(Mathf.Abs(coord.x), Mathf.Abs(coord.y));

            ApplyMovement(coord, origCoord, _pipeCtrl.Multiplier, _pipeCtrl.AxisType);
        }

        public override void OnTouchExit()
        {
            HandlePipeParentChanges(true);
            _lockRotation = MeshMovementType.None;
        }

        private void ApplyMovement(Vector2 coord, Vector2 origCoord, float multi, AxisType axisType)
        {
            float x = _edgeCenPivT.transform.localPosition.x;
            float y = _edgeCenPivT.transform.localPosition.y;
            float z = _edgeCenPivT.transform.localPosition.z;

            if (axisType == AxisType.ZPos || axisType == AxisType.ZNeg)
            {
                if (coord.x > coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.X))
                {
                    if (_pipeEdgeType == PipeEdgeType.C || _pipeEdgeType == PipeEdgeType.B)
                        return;
                    
                    float val = axisType == AxisType.ZPos ? multi * origCoord.x : multi * -origCoord.x;
                    
                    Vector3 p = new Vector3(x + val, y, z);
                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        _edgeCenPivT.transform.localPosition = p;

                    float angle = CalculateAngle(val)/2f;
                    //_edgeCenPivT.transform.localEulerAngles = new Vector3(_edgeCenPivT.localEulerAngles.x, _edgeCenPivT.localEulerAngles.y - angle, _edgeCenPivT.localEulerAngles.z);

                    if (_pipeEdgeType == PipeEdgeType.A)
                        _pipeCtrl.ApplyRotationOnOtherEdgesAndHose(PipeEdgeType.A, PipeEdgeType.B, PipeHoseType.BC, angle, true);
                    else if (_pipeEdgeType == PipeEdgeType.D)
                        _pipeCtrl.ApplyRotationOnOtherEdgesAndHose(PipeEdgeType.D, PipeEdgeType.C, PipeHoseType.BC, angle, true);

                    _lockRotation = MeshMovementType.X;
                }
                else if (coord.x < coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.Z))
                {
                    HandlePipeParentChanges(false);
                    Vector3 p = new Vector3(x, y, z + (multi * -origCoord.y));

                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        _edgeCenPivT.transform.localPosition = p;

                    _lockRotation = MeshMovementType.Z;
                }
            }
            else if (axisType == AxisType.XPos || axisType == AxisType.XNeg)
            {
                if (coord.x > coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.Y))
                {
                    float val = axisType == AxisType.XPos ? multi * origCoord.x :multi * -origCoord.x;
                    Vector3 p = new Vector3(x, y + val, z);

                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        _edgeCenPivT.transform.localPosition = p;

                    //float angle = CalculateAngle(val);
                    //_edgeCenPivT.transform.localEulerAngles = new Vector3(_edgeCenPivT.localEulerAngles.x + angle, _edgeCenPivT.localEulerAngles.y, _edgeCenPivT.localEulerAngles.z);

                    //if (_pipeEdgeType == PipeEdgeType.A)
                    //    _pipeCtrl.ApplyRotationOnOtherEdgesAndHose(PipeEdgeType.A, PipeEdgeType.B, PipeHoseType.BC, angle, true);
                    //else if (_pipeEdgeType == PipeEdgeType.D)
                    //    _pipeCtrl.ApplyRotationOnOtherEdgesAndHose(PipeEdgeType.D, PipeEdgeType.C, PipeHoseType.BC, angle, true);

                    _lockRotation = MeshMovementType.Y;
                }
                else if (coord.x < coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.Z))
                {
                    HandlePipeParentChanges(false);
                    Vector3 p = new Vector3(x, y, z + (multi * -origCoord.y));

                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        _edgeCenPivT.transform.localPosition = p;

                    _lockRotation = MeshMovementType.Z;
                }
            }
            else if (axisType == AxisType.YPos || axisType == AxisType.YNeg)
            {
                if (coord.x > coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.X))
                {
                    float val = axisType == AxisType.YPos ? multi * origCoord.x : multi * origCoord.x;
                    Vector3 p = new Vector3(x + val, y, z);

                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        _edgeCenPivT.transform.localPosition = p;

                    float angle = CalculateAngle(val)/2;
                    //_edgeCenPivT.transform.localEulerAngles = new Vector3(_edgeCenPivT.localEulerAngles.x, _edgeCenPivT.localEulerAngles.y - angle, _edgeCenPivT.localEulerAngles.z);

                    if (_pipeEdgeType == PipeEdgeType.A)
                        _pipeCtrl.ApplyRotationOnOtherEdgesAndHose(PipeEdgeType.A, PipeEdgeType.B, PipeHoseType.BC, angle, true);
                    else if (_pipeEdgeType == PipeEdgeType.D)
                        _pipeCtrl.ApplyRotationOnOtherEdgesAndHose(PipeEdgeType.D, PipeEdgeType.C, PipeHoseType.BC, angle, true);

                    _lockRotation = MeshMovementType.X;
                }
                else if (coord.x < coord.y && (_lockRotation == MeshMovementType.None || _lockRotation == MeshMovementType.Y))
                {
                    float val = axisType == AxisType.YPos ? multi * origCoord.y : multi * -origCoord.y;
                    Vector3 p = new Vector3(x, y + val, z);

                    if (_pipeCtrl.ApplyLimOnEdges)
                    {
                        if (_edgeTUpdater.UpdatePosWithLimit(p) == false)
                            return;
                    }
                    else
                        _edgeCenPivT.transform.localPosition = p;

                    //float angle = CalculateAngle(val);
                    //_edgeCenPivT.transform.localEulerAngles = new Vector3(_edgeCenPivT.localEulerAngles.x + angle, _edgeCenPivT.localEulerAngles.y, _edgeCenPivT.localEulerAngles.z);

                    //if (_pipeEdgeType == PipeEdgeType.A)
                    //    _pipeCtrl.ApplyRotationOnOtherEdgesAndHose(PipeEdgeType.A, PipeEdgeType.B, PipeHoseType.BC, angle, false);
                    //else if (_pipeEdgeType == PipeEdgeType.D)
                    //    _pipeCtrl.ApplyRotationOnOtherEdgesAndHose(PipeEdgeType.D, PipeEdgeType.C, PipeHoseType.BC, angle, false);

                    _lockRotation = MeshMovementType.Y;
                }
            }
        }

        private void HandlePipeParentChanges(bool reset)
        {
            if (reset == false)
            {
                if (_pipeEdgeType == PipeEdgeType.A)
                    _pipeCtrl.ChangeParents(ParentChangeBeh.Do, PipeEdgeType.B, PipeHoseType.None, PipeEdgeType.A);
                else if(_pipeEdgeType == PipeEdgeType.B)
                    _pipeCtrl.ChangeParents(ParentChangeBeh.Do, PipeEdgeType.A, PipeHoseType.None, PipeEdgeType.B);

                else if (_pipeEdgeType == PipeEdgeType.C)
                    _pipeCtrl.ChangeParents(ParentChangeBeh.Do, PipeEdgeType.D, PipeHoseType.None, PipeEdgeType.C);
                else if (_pipeEdgeType == PipeEdgeType.D)
                    _pipeCtrl.ChangeParents(ParentChangeBeh.Do, PipeEdgeType.C, PipeHoseType.None, PipeEdgeType.D);
            }
            else
            {
                if (_pipeEdgeType == PipeEdgeType.A)
                    _pipeCtrl.ChangeParents(ParentChangeBeh.Reset, PipeEdgeType.B, PipeHoseType.None, PipeEdgeType.None);
                else if(_pipeEdgeType == PipeEdgeType.B)
                    _pipeCtrl.ChangeParents(ParentChangeBeh.Reset, PipeEdgeType.A, PipeHoseType.None, PipeEdgeType.None);

                else if (_pipeEdgeType == PipeEdgeType.C)
                    _pipeCtrl.ChangeParents(ParentChangeBeh.Reset, PipeEdgeType.D, PipeHoseType.None, PipeEdgeType.None);
                else if (_pipeEdgeType == PipeEdgeType.D)
                    _pipeCtrl.ChangeParents(ParentChangeBeh.Reset, PipeEdgeType.C, PipeHoseType.None, PipeEdgeType.None);
            }
        }
    }
}
