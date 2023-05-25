using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private TouchControls _touchControls;
    private bool _firstFingerHold = false;

    private GameObject hoveredGO;
    private enum TouchState { TOUCH, NONE };
    private TouchState touch_state = TouchState.NONE;

    private Collider _hitCollider = null;

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
    }

    private void Update()
    {
        HandleMeshMovement();
    }

    private void OnStartFirstTouch(InputAction.CallbackContext ctx)
    {
            _firstFingerHold = true;
    }

    private void OnEndFirstTouch(InputAction.CallbackContext ctx)
    {
            _firstFingerHold = false;
    }

    private void HandleMeshMovement()
    {
        if (_firstFingerHold)
        {
            if (touch_state == TouchState.NONE)
            {
                Vector2 coord = _touchControls.Touch.FirstFingerDeltaPosition.ReadValue<Vector2>();

                RaycastHit hitInfo = new RaycastHit(); 
                Ray ray = Camera.main.ScreenPointToRay(coord);

                if (Physics.Raycast(ray, out hitInfo))
                {
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
}
