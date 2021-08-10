using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputController : MonoBehaviour
{
    PlayerInput _gameInputSystem;
    PlayerController _player;
    CameraController _camera;

    PlayerInput GameInputSystem { get { return _gameInputSystem; } }
    public void Init(GameObject player)
    {
        _gameInputSystem = Managers.InputMgr.GameInputSystem;
        _player = player.GetComponent<PlayerController>();
        _camera = Managers.CameraMgr.GameCam;
        DeactivatePlayerInput();
    }
    public void DeactivatePlayerInput()
    {
        if(_gameInputSystem.actions["MouseClick"].enabled == true)
        {
            _gameInputSystem.actions["MouseClick"].Disable();
        }
        if(_gameInputSystem.actions["MousePosition"].enabled == true)
        {
            _gameInputSystem.actions["MousePosition"].Disable();
        }
    }
    public void ActivatePlayerInput()
    {
        if (_gameInputSystem.actions["MouseClick"].enabled == false)
        {
            _gameInputSystem.actions["MouseClick"].Enable();
        }
        if (_gameInputSystem.actions["MousePosition"].enabled == false)
        {
            _gameInputSystem.actions["MousePosition"].Enable();
        }
    }
    public void UpdateMouseScreenPosition(InputAction.CallbackContext context)
    {
        _player.UpdateMouseScreenPosition(context);
    }
  
    public void OnClicked(InputAction.CallbackContext context)
    {
        _player.OnClicked(context);
    }
    public void UpdateManualPosition(InputAction.CallbackContext context)
    {
        _camera.UpdateManualPosition(context);
    }
   public void UpdateCameraSize(InputAction.CallbackContext context)
    {
        _camera.UpdateCameraSize(context);
    }
}
