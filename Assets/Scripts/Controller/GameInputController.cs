using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameInputController : MonoBehaviour
{
    PlayerInput _gameInputSystem;
    InputAction _onMouseClick;
    InputAction _onMouseMove;
    PlayerController _player;
    CameraController _camera;
    RaycastHit2D _hit;
    #region UI
    PointerEventData _pointerEventData;
    List<RaycastResult> _results;
    Imouse _hoverTarget;
    public Imouse HoverTarget { get { return _hoverTarget; } }
    #endregion
    Imouse _clickTarget;
    public void Init(GameObject player)
    {
        _gameInputSystem = Managers.InputMgr.GameInputSystem;
        _onMouseClick = _gameInputSystem.actions["OnMouseClick"];
        _onMouseMove = _gameInputSystem.actions["OnMouseMove"];

        _onMouseClick.started += OnClickStarted;
        _onMouseClick.canceled += OnClickCanceled;
        _onMouseMove.performed += OnMouseMove;

        _player = player.GetComponent<PlayerController>();
        _camera = Managers.CameraMgr.GameCamController;

        _pointerEventData = new PointerEventData(null);
        _results = new List<RaycastResult>();
        DeactivatePlayerInput();
    }
    public void DeactivatePlayerInput()
    {
        if(_onMouseClick.enabled == true)
        {
            _onMouseClick.Disable();
        }
        if(_onMouseMove.enabled == true)
        {
            _onMouseMove.Disable();
        }
    }
    public void ActivatePlayerInput()
    {
        if (_onMouseClick.enabled == false)
        {
            _onMouseClick.Enable();
        }
        if (_onMouseMove.enabled == false)
        {
            _onMouseMove.Enable();
        }
    }

    public void OnClickStarted(InputAction.CallbackContext context)
    {
        Imouse temp = GetTarget();
        if(temp != null)
        {
            _clickTarget = temp;
            _clickTarget.OnMouseDown(context);
            _onMouseMove.performed += _clickTarget.OnDrag;
        }
    }
    public void OnClickCanceled(InputAction.CallbackContext context)
    {
        if(_clickTarget != null)
        {
            _onMouseMove.performed -= _clickTarget.OnDrag;
            _clickTarget.OnMouseUp(context);
        }
    }
    public void OnMouseMove(InputAction.CallbackContext context)
    {
        Managers.InputMgr.MouseScreenPosition = context.ReadValue<Vector2>();
        _hoverTarget = GetTarget();
        _hoverTarget?.OnMouseHover(context);
    }
    private Imouse GetTarget()
    {
        _pointerEventData.position = Managers.InputMgr.MouseScreenPosition;
        EventSystem.current.RaycastAll(_pointerEventData, _results);
        if(_results.Count > 0) { return _results[0].gameObject.GetComponent<Imouse>(); }
        else
        {
            _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Managers.InputMgr.MouseScreenPosition), Vector2.zero);
            return _hit.collider?.GetComponent<Imouse>();
        }
    }

    #region camera
    public void UpdateManualPosition(InputAction.CallbackContext context)
    {
        _camera.UpdateManualPosition(context);
    }
    public void UpdateCameraSize(InputAction.CallbackContext context)
    {
        _camera.UpdateCameraSize(context);
    }
    #endregion





}
