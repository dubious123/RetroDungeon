using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameInputController : MonoBehaviour
{
    
    PlayerInput _gameInputSystem;
    InputAction _onMouseClick;
    InputAction _onMouseMove;
    InputAction _onMouseRightClick;
    InputAction _onMouseScroll;
    InputAction _interactionKey;
    PlayerController _player;
    GameCamController _camera;
    RaycastHit2D _hit;
    #region UI
    PointerEventData _pointerEventData;
    List<RaycastResult> _results;
    Imouse _hoverTarget;
    UnityEvent _rightClickEvent;
    UnityEvent _interactionEvent;
    public Imouse HoverTarget { get { return _hoverTarget; } }
    public UnityEvent RightClickEvent { get { return _rightClickEvent; } }
    public UnityEvent InteractionEvent { get { return _interactionEvent; } }
    #endregion
    Imouse _clickTarget;
    public void Init(GameObject player)
    {
        _rightClickEvent = new UnityEvent();
        _interactionEvent = new UnityEvent();
        _gameInputSystem = Managers.InputMgr.GameInputSystem;
        _onMouseClick = _gameInputSystem.actions["OnMouseClick"];
        _onMouseMove = _gameInputSystem.actions["OnMouseMove"];
        _onMouseRightClick = _gameInputSystem.actions["OnMouseRightClick"];
        _onMouseScroll = _gameInputSystem.actions["CameraScrollMovement"];
        _interactionKey = _gameInputSystem.actions["InteractionKey"];
        _onMouseClick.started += OnClickStarted;
        _onMouseClick.canceled += OnClickCanceled;
        _onMouseRightClick.started += OnRightClickStarted;
        _onMouseRightClick.canceled += OnRightClickCanceled;
        _onMouseMove.performed += OnMouseMove;
        _interactionKey.started += OnInteractionKeyStarted;
        _interactionKey.canceled += OnInteractionKeyCanceled;

        _player = player.GetComponent<PlayerController>();
        _camera = Managers.CameraMgr.GameCamController;

        _pointerEventData = new PointerEventData(null);
        _results = new List<RaycastResult>();

        //TestMode
        _gameInputSystem.actions["EnterTestMode"].performed += Managers.TestMgr.StartTest;

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
    public void DeactivateCameraScroll()
    {
        if (_onMouseScroll.enabled == true)
        {
            _onMouseScroll.Disable();
        }
    }
    public void ActivateCameraScroll()
    {
        if (_onMouseScroll.enabled == false)
        {
            _onMouseScroll.Enable();
        }

    }
    private void OnClickStarted(InputAction.CallbackContext context)
    {
        Imouse temp = GetTarget();
        if(temp != null)
        {
            _clickTarget = temp;
            _clickTarget.OnMouseDown(context);
            _onMouseMove.performed += _clickTarget.OnDrag;
        }
    }
    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        if(_clickTarget != null)
        {
            _onMouseMove.performed -= _clickTarget.OnDrag;
            _clickTarget.OnMouseUp(context);
        }
    }
    private void OnRightClickStarted(InputAction.CallbackContext context)
    {
        _rightClickEvent.Invoke();

    }
    private void OnRightClickCanceled(InputAction.CallbackContext context)
    {
        _rightClickEvent.RemoveAllListeners();
    }
    private void OnMouseMove(InputAction.CallbackContext context)
    {
        Managers.InputMgr.MouseScreenPosition = context.ReadValue<Vector2>();
        _hoverTarget = GetTarget();
        _hoverTarget?.OnMouseHover(context);
    }
    private void OnInteractionKeyStarted(InputAction.CallbackContext context)
    {
        _interactionEvent.Invoke();
    }
    private void OnInteractionKeyCanceled(InputAction.CallbackContext context)
    {
        _interactionEvent.RemoveAllListeners();
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
    public void Clear()
    {
        _onMouseClick.started -= OnClickStarted;
        _onMouseClick.canceled -= OnClickCanceled;
        _onMouseRightClick.started -= OnRightClickStarted;
        _onMouseRightClick.canceled -= OnRightClickCanceled;
        _onMouseMove.performed -= OnMouseMove;
    }




}
