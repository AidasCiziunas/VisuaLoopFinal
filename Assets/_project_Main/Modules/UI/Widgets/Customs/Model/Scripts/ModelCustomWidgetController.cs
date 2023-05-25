using Doozy.Runtime.Reactor;
using Doozy.Runtime.Reactor.Animators;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace SyedAli.Main.UIModule.Widgets
{
    public class ModelCustomWidgetController : MonoBehaviour
    {
        public EventHandler<ModelMoveBehaviourEventArgs> ModelMoveBehaviour_EventHandler;
        public EventHandler<ModelZoomBehaviourEventArgs> ModelZoomBehaviour_EventHandler;

        public ModelCustomWidgetProperties Props { get => _props; }

        [SerializeField] Transform _cameraT;

        [Space(10)]
        [SerializeField] Transform _modelParentT;
        [SerializeField] float _zoomScaleFactor;
        [SerializeField] float _meshMoveMulti;

        private ModelCustomWidgetProperties _props;

        private PipeModelController _currentModelCtrl;
        private GameObject _currentModelGo;

        private GameObject hoveredGO;
        private Collider _hitCollider = null;
        private TouchState touch_state = TouchState.NONE;

        private TouchControls _touchControls;
        private bool _firstFingerHold = false;
        private bool _secondFingerHold = false;
        private ModelRotationType _lockRotation = ModelRotationType.None;
        private float _rotationAnglePerFrame = 5.0f;
        private Vector3 _modelOrigScale;
        private AxisType _currentAxisType;
        private float _prevDis = 0.0f;

        private void Awake()
        {
            _touchControls = new TouchControls();
        }

        private void OnEnable()
        {
            _touchControls.Enable();
        }

        private void OnDisable()
        {
            _touchControls.Disable();
        }

        private void Start()
        {
            _touchControls.Touch.FirstFingerHold.started += ctx => OnStartFirstTouch(ctx);
            _touchControls.Touch.FirstFingerHold.canceled += ctx => OnEndFirstTouch(ctx);

            _touchControls.Touch.SecondFingerHold.started += ctx => OnStartSecondTouch(ctx);
            _touchControls.Touch.SecondFingerHold.canceled += ctx => OnEndSecondTouch(ctx);
        }

        private void FixedUpdate()
        {
            if (_props.ModelWidMode == ModelWidgetMode.ThreeD)
                HandleThreeDMode();
            else if (_props.ModelWidMode == ModelWidgetMode.Move)
                HandleMoveMode();
            else if (_props.ModelWidMode == ModelWidgetMode.Zoom)
                HandleZoomMode();
        }

        public void SetData(ModelCustomWidgetProperties props)
        {
            _props = props;

            SetupWidget();
        }

        public void Refresh(ModelCustomWidgetProperties props)
        {
            SetData(props);
        }

        public void ResetModel(ResetType resetType)
        {
            switch(resetType)
            {
                case ResetType.ThreeD:
                    _currentModelCtrl.ResetModel();
                    break;
                case ResetType.Move:
                    RotateAtStartPoint(false);
                    break;
            }
        }

        public void ResetModel(ResetType resetType, float zoomVal)
        {
            _props = new ModelCustomWidgetProperties(_props.ModelWidMode, _props.InstantChangeAxis, _props.ModelGo, zoomVal, _props.InstantChangeAxisDirection);

            switch (resetType)
            {
                case ResetType.Zoom:
                    SetupZoomMode(zoomVal);
                    break;
            }
        }

        public void Visibility(bool status)
        {
            this.gameObject.SetActive(status);
        }

        private void OnStartFirstTouch(InputAction.CallbackContext ctx)
        {
            if (_props.ModelWidMode == ModelWidgetMode.ThreeD || _props.ModelWidMode == ModelWidgetMode.Move || _props.ModelWidMode == ModelWidgetMode.Zoom)
            {
                _firstFingerHold = true;
            }
        }

        private void OnEndFirstTouch(InputAction.CallbackContext ctx)
        {
            if (_props.ModelWidMode == ModelWidgetMode.ThreeD || _props.ModelWidMode == ModelWidgetMode.Zoom)
            {
                _firstFingerHold = false;
            }
            else if (_props.ModelWidMode == ModelWidgetMode.Move)
            {
                _firstFingerHold = false;
                _lockRotation = ModelRotationType.None;
                ModelMoveBehaviour_EventHandler?.Invoke(this, new ModelMoveBehaviourEventArgs(false));
            }
        }

        private void OnStartSecondTouch(InputAction.CallbackContext ctx)
        {
            if (_props.ModelWidMode == ModelWidgetMode.Move || _props.ModelWidMode == ModelWidgetMode.Zoom)
            {
                _secondFingerHold = true;
            }
        }

        private void OnEndSecondTouch(InputAction.CallbackContext ctx)
        {
            if (_props.ModelWidMode == ModelWidgetMode.Move)
            {
                _secondFingerHold = false;
                _lockRotation = ModelRotationType.None;
                ModelMoveBehaviour_EventHandler?.Invoke(this, new ModelMoveBehaviourEventArgs(false));
            }
            else if (_props.ModelWidMode == ModelWidgetMode.Zoom)
            {
                _secondFingerHold = false;
            }
        }

        private void SetupWidget()
        {
            switch(_props.ModelWidMode)
            {
                case ModelWidgetMode.ShowOnly:
                    SetupShowOnlyMode();
                    break;
                case ModelWidgetMode.Move:
                    SetupMoveMode();
                    break;
                case ModelWidgetMode.ThreeD:
                    SetupThreeDMode();
                    break;
                case ModelWidgetMode.Zoom:
                    SetupZoomMode(_props.Zoom);
                    break;
            }

            switch(_props.InstantChangeAxis)
            {
                case InstantChangeAxis.InstXAxis:
                    SetupInstantAxisRotation(_props.InstantChangeAxis, _props.InstantChangeAxisDirection);
                    break;
                case InstantChangeAxis.InstYAxis:
                    SetupInstantAxisRotation(_props.InstantChangeAxis, _props.InstantChangeAxisDirection);
                    break;
                case InstantChangeAxis.InstZAxis:
                    SetupInstantAxisRotation(_props.InstantChangeAxis, _props.InstantChangeAxisDirection);
                    break;
            }
        }

        private void SetupShowOnlyMode()
        {
            if (_currentModelGo != null)
                _currentModelCtrl.ChangeVisibilityStatus(false);

            _props.ModelGo.transform.SetParent(_modelParentT, false);
            _currentModelGo = _props.ModelGo;
            _currentModelCtrl = _props.ModelGo.GetComponent<PipeModelController>();

            if (_currentModelCtrl.Props == null)
                _currentModelCtrl.Setup(new PipeModelControllerProperties(_meshMoveMulti, _currentAxisType));

            _modelOrigScale = _currentModelCtrl.LocalScale;

            _currentModelCtrl.ChangeVisibilityStatus(true);
        }

        private void SetupMoveMode()
        {
            SetupInstantAxisRotation(InstantChangeAxis.InstZAxis, true);
            _currentModelCtrl.ChangeHosesInteractibility(false);
        }

        private void SetupThreeDMode()
        {
            if (_props.InstantChangeAxis != InstantChangeAxis.None)
            {
                RotateAtStartPoint(true);
            }
        }

        private void RotateAtStartPoint(bool hoseInteract)
        {
            Vector3 targetAngle = Vector3.zero;
            _currentModelCtrl.DoRotationAnim(targetAngle, () => _currentModelCtrl.ChangeHosesInteractibility(hoseInteract));
            _currentModelCtrl.Refresh(new PipeModelControllerProperties(_meshMoveMulti, AxisType.ZPos));
        }

        private void SetupZoomMode(float zoom)
        {
            _currentModelCtrl.ChangeHosesInteractibility(false);
            _currentModelCtrl.ChangeLocalScaleInstant(new Vector3(_modelOrigScale.x + (zoom * _zoomScaleFactor), _modelOrigScale.y + (zoom * _zoomScaleFactor), _modelOrigScale.z + (zoom * _zoomScaleFactor)));
        }

        private void HandleZoomMode()
        {
            if (_firstFingerHold && _secondFingerHold)
            {
                Vector2 firstPosCoord = _touchControls.Touch.FirstFinger2dPosition.ReadValue<Vector2>();
                Vector2 secondPosCoord = _touchControls.Touch.SecondFinger2dPosition.ReadValue<Vector2>();

                Vector2 firstDeltaCoord = _touchControls.Touch.FirstFingerDeltaPosition.ReadValue<Vector2>();
                Vector2 secondDeltaCoord = _touchControls.Touch.SecondFingerPosition.ReadValue<Vector2>();

                Vector2 firstPrevPos = firstPosCoord - firstDeltaCoord;
                Vector2 secondPrevPos = secondPosCoord - secondDeltaCoord;

                float prevMag = (firstPrevPos - secondPrevPos).magnitude;
                float currMag = (firstPosCoord - secondPosCoord).magnitude;

                float diff = currMag - prevMag;

                if (diff > 0)
                {
                    ModelZoomBehaviour_EventHandler?.Invoke(this, new ModelZoomBehaviourEventArgs(ZoomType.PinchIn));
                }
                else if (diff <0)
                {
                    ModelZoomBehaviour_EventHandler?.Invoke(this, new ModelZoomBehaviourEventArgs(ZoomType.PinchOut));
                }
            }
        }

        private void HandleMoveMode()
        {
            if (_firstFingerHold && _secondFingerHold == false)
            {
                Vector2 coord = _touchControls.Touch.FirstFingerDeltaPosition.ReadValue<Vector2>();
                Vector2 origCoord = coord;

                //Debug.Log(coord);
                coord = new Vector2(Mathf.Abs(coord.x), Mathf.Abs(coord.y));

                if (coord.x > coord.y)
                {
                    if (origCoord.x > 0 && (_lockRotation == ModelRotationType.None || _lockRotation == ModelRotationType.XPos || _lockRotation == ModelRotationType.XNeg))
                    {
                        //Debug.Log("X Positive");
                        _lockRotation = ModelRotationType.XPos;
                        ProvideRotationToModel(ModelRotationType.XPos);
                    }
                    else if (origCoord.x < 0 && (_lockRotation == ModelRotationType.None || _lockRotation == ModelRotationType.XPos || _lockRotation == ModelRotationType.XNeg))
                    {
                        //Debug.Log("X Negative");
                        _lockRotation = ModelRotationType.XNeg;
                        ProvideRotationToModel(ModelRotationType.XNeg);
                    }
                }
                else if (coord.x < coord.y)
                {
                    if (origCoord.y > 0 && (_lockRotation == ModelRotationType.None || _lockRotation == ModelRotationType.YPos || _lockRotation == ModelRotationType.YNeg))
                    {
                        //Debug.Log("Y Positive");
                        _lockRotation = ModelRotationType.YPos;
                        ProvideRotationToModel(ModelRotationType.YPos);
                    }
                    else if (origCoord.y < 0 && (_lockRotation == ModelRotationType.None || _lockRotation == ModelRotationType.YPos || _lockRotation == ModelRotationType.YNeg))
                    {
                        //Debug.Log("Y Negative");
                        _lockRotation = ModelRotationType.YNeg;
                        ProvideRotationToModel(ModelRotationType.YNeg);
                    }
                }

                ModelMoveBehaviour_EventHandler?.Invoke(this, new ModelMoveBehaviourEventArgs(true));
            }
            else if (_firstFingerHold && _secondFingerHold)
            {
                Vector2 firstCoord = _touchControls.Touch.FirstFingerDeltaPosition.ReadValue<Vector2>();
                Vector2 secondCoord = _touchControls.Touch.SecondFingerPosition.ReadValue<Vector2>();

                Vector2 normFirst = firstCoord.normalized;
                Vector2 normSecond = secondCoord.normalized;

                float val = Vector2.Dot(normFirst, normSecond);

                Debug.Log(val);
                if (val > 0.0f)
                {
                    if (firstCoord.x > 0) //Same Direction
                    {
                        //Debug.Log("Rotation: X Positive");
                        _lockRotation = ModelRotationType.ZAntiClock;
                        ProvideRotationToModel(ModelRotationType.ZAntiClock);
                    }
                    else
                    {
                        //Debug.Log("Rotation: X Negative");
                        _lockRotation = ModelRotationType.ZClock;
                        ProvideRotationToModel(ModelRotationType.ZClock);
                    }
                }
                else if (val < 0.0f) //Opposite Direction
                {
                    if (firstCoord.x > 0 && secondCoord.x < 0)
                    {
                        //Debug.Log("Rotation: Clock");
                        _lockRotation = ModelRotationType.ZClock;
                        ProvideRotationToModel(ModelRotationType.ZClock);
                    }
                    else if (firstCoord.x < 0 && secondCoord.x > 0)
                    {
                        //Debug.Log("Rotation: AntiClock");
                        _lockRotation = ModelRotationType.ZAntiClock;
                        ProvideRotationToModel(ModelRotationType.ZAntiClock);
                    }
                }

                ModelMoveBehaviour_EventHandler?.Invoke(this, new ModelMoveBehaviourEventArgs(true));
            }
        }

        private void HandleThreeDMode()
        {
            if (_firstFingerHold)
            {
                if (touch_state == TouchState.NONE)
                {
                    //Debug.Log("Touched");
                    Vector2 coord = _touchControls.Touch.FirstFinger2dPosition.ReadValue<Vector2>();

                    RaycastHit hitInfo = new RaycastHit();
                    Ray ray = Camera.main.ScreenPointToRay(coord);

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        //Debug.Log("Hit: " + hitInfo.collider.gameObject);

                        _hitCollider = hitInfo.collider;
                        hitInfo.collider.SendMessage("OnTouchEnter", SendMessageOptions.DontRequireReceiver);
                        hoveredGO = hitInfo.collider.gameObject;
                    }

                    touch_state = TouchState.TOUCH;
                }

                if (_hitCollider != null && touch_state == TouchState.TOUCH)
                {
                    _hitCollider.SendMessage("OnTouchDrag", SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                if (hoveredGO != null && touch_state == TouchState.TOUCH)
                {
                    hoveredGO.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);
                    _hitCollider = null;
                    hoveredGO = null;
                }
                touch_state = TouchState.NONE;
            }
        }

        private void SetupInstantAxisRotation(InstantChangeAxis instChangeAxis, bool instChangeAxisDir)
        {
            TargetResolution tarRes = ManagersSignals.Get<AppManagerSignal.GetTargetDevice>().Dispatch();

            if (tarRes == TargetResolution.Tablet)
                HandleCamMovementForTablet(instChangeAxis, instChangeAxisDir);
            else if (tarRes == TargetResolution.Phone)
                HandleCamMovementForPhone(instChangeAxis, instChangeAxisDir);            
        }

        private void ProvideRotationToModel(ModelRotationType rotationType)
        {
            _currentModelCtrl.DoRotateAroundInstant(_currentModelCtrl.Pos, DetermineRotationSide(rotationType), _rotationAnglePerFrame);
            _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.None));
        }

        private Vector3 DetermineRotationSide(ModelRotationType rotationType)
        {
            if (rotationType == ModelRotationType.XPos)
            {
                Vector3 vector3 = Vector3.Cross(Vector3.forward, Vector3.left);
                return vector3;
            }
            else if (rotationType == ModelRotationType.XNeg)
            {
                Vector3 vector3 = Vector3.Cross(Vector3.forward, Vector3.right);
                return vector3;
            }
            else if (rotationType == ModelRotationType.YPos)
            {
                Vector3 vector3 = Vector3.Cross(Vector3.up, Vector3.forward);
                return vector3;
            }
            else if (rotationType == ModelRotationType.YNeg)
            {
                Vector3 vector3 = Vector3.Cross(Vector3.up, Vector3.back);
                return vector3;
            }
            else if (rotationType == ModelRotationType.ZClock)
            {
                Vector3 vector3 = Vector3.Cross(Vector3.left, Vector3.down);
                return vector3;
            }
            else if (rotationType == ModelRotationType.ZAntiClock)
            {
                Vector3 vector3 = Vector3.Cross(Vector3.left, Vector3.up);
                return vector3;
            }
            return Vector3.zero;
        }

        private void HandleCamMovementForTablet(InstantChangeAxis instChangeAxis, bool instChangeAxisDir)
        {
            switch (instChangeAxis)
            {
                case InstantChangeAxis.InstXAxis:
                    if (instChangeAxisDir)
                    {
                        _cameraT.localPosition = new Vector3(1070, 0, 924);
                        _cameraT.localEulerAngles = new Vector3(0, -90, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.XPos));
                    }
                    else
                    {
                        _cameraT.localPosition = new Vector3(-750, 0, 1080);
                        _cameraT.localEulerAngles = new Vector3(0, 90, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.XNeg));
                    }
                    break;
                case InstantChangeAxis.InstYAxis:
                    if (instChangeAxisDir)
                    {
                        _cameraT.localPosition = new Vector3(0, 700, 1160);
                        _cameraT.localEulerAngles = new Vector3(90, 0, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.YPos));
                    }
                    else
                    {
                        _cameraT.localPosition = new Vector3(0, -624, 840);
                        _cameraT.localEulerAngles = new Vector3(270, 0, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.YNeg));
                    }
                    break;
                case InstantChangeAxis.InstZAxis:
                    if (instChangeAxisDir)
                    {
                        _cameraT.localPosition = new Vector3(0, 0, 300);
                        _cameraT.localEulerAngles = new Vector3(0, 0, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.ZPos));
                    }
                    else
                    {
                        _cameraT.localPosition = new Vector3(140, 0, 1780);
                        _cameraT.localEulerAngles = new Vector3(0, 180, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.ZNeg));
                    }
                    break;
            }

            _currentAxisType = _currentModelCtrl.AxisType;
        }

        private void HandleCamMovementForPhone(InstantChangeAxis instChangeAxis, bool instChangeAxisDir)
        {
            switch (instChangeAxis)
            {
                case InstantChangeAxis.InstXAxis:
                    if (instChangeAxisDir)
                    {
                        _cameraT.localPosition = new Vector3(1070, 0, 1000);
                        _cameraT.localEulerAngles = new Vector3(0, -90, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.XPos));
                    }
                    else
                    {
                        _cameraT.localPosition = new Vector3(-750, 0, 1000);
                        _cameraT.localEulerAngles = new Vector3(0, 90, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.XNeg));
                    }
                    break;
                case InstantChangeAxis.InstYAxis:
                    if (instChangeAxisDir)
                    {
                        _cameraT.localPosition = new Vector3(0, 700, 1100);
                        _cameraT.localEulerAngles = new Vector3(90, 0, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.YPos));
                    }
                    else
                    {
                        _cameraT.localPosition = new Vector3(0, -624, 900);
                        _cameraT.localEulerAngles = new Vector3(270, 0, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.YNeg));
                    }
                    break;
                case InstantChangeAxis.InstZAxis:
                    if (instChangeAxisDir)
                    {
                        _cameraT.localPosition = new Vector3(0, 0, 300);
                        _cameraT.localEulerAngles = new Vector3(0, 0, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.ZPos));
                    }
                    else
                    {
                        _cameraT.localPosition = new Vector3(0, 0, 1780);
                        _cameraT.localEulerAngles = new Vector3(0, 180, 0);
                        _currentModelCtrl.Refresh(new PipeModelControllerProperties(_currentModelCtrl.Props.Multiplier, AxisType.ZNeg));
                    }
                    break;
            }

            _currentAxisType = _currentModelCtrl.AxisType;
        }
    }
}
