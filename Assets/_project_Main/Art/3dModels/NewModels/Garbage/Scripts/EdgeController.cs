using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum ModelRotationType
{
    None, X, Y, Z
}

public class EdgeController : MonoBehaviour
{
    public Transform _edgeT;
    public float _multiplier;

    private TouchControls _touchControls;
    private bool _firstFingerHold = false;

    private GameObject hoveredGO;
    private enum TouchState { TOUCH, NONE };
    private TouchState touch_state = TouchState.NONE;

    private ModelRotationType _lockRotation = ModelRotationType.None;

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

    public void OnTouchEnter()
    {
        Debug.Log("OnTouchEnter");
    }

    public void OnTouchDrag()
    {
        Vector2 coord = _touchControls.Touch.FirstFingerDeltaPosition.ReadValue<Vector2>();
        Debug.Log("OnTouchDrag : " + coord);

        Vector2 origCoord = coord;

        coord = new Vector2(Mathf.Abs(coord.x), Mathf.Abs(coord.y));

        if (coord.x > coord.y && (_lockRotation == ModelRotationType.None || _lockRotation == ModelRotationType.X))
        {
            _edgeT.transform.localPosition = new Vector3(_edgeT.transform.localPosition.x + (_multiplier * origCoord.x), _edgeT.transform.localPosition.y, _edgeT.transform.localPosition.z );
            _lockRotation = ModelRotationType.X;
        }
        else if (coord.x < coord.y && (_lockRotation == ModelRotationType.None || _lockRotation == ModelRotationType.Z))
        {
            _edgeT.transform.localPosition = new Vector3(_edgeT.transform.localPosition.x, _edgeT.transform.localPosition.y, _edgeT.transform.localPosition.z + (_multiplier * origCoord.y));
            _lockRotation = ModelRotationType.Z;
        }
    }

    public void OnTouchExit()
    {
        Debug.Log("OnTouchExit");
        _lockRotation = ModelRotationType.None;
    }
}
