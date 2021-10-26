using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class InputManager
{
    Grid _grid;
    PlayerInput _gameInputSystem;
    GameInputController _gameInputController;
    Camera _mainCamera;
    public Camera MainCamera
    {
        get
        {
            if(_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }
            return _mainCamera;
        }
    }
    public PlayerInput GameInputSystem 
    { 
        get 
        { 
            if(_gameInputSystem == null) 
            {
                _gameInputSystem = GameController.GetComponent<PlayerInput>();
            }
            return _gameInputSystem; 
        } 
    }
    public GameInputController GameController 
    { 
        get 
        { 
            if(_gameInputController == null)
            {
                _gameInputController = Managers.ResourceMgr.Instantiate("UI/GameInputController").GetComponent<GameInputController>();
            }
            return _gameInputController; 
        } 
    }
    public Vector2 MouseScreenPos { get; set; }
    public void Init()
    {   
    }
    public void InitControllers(GameObject player)
    {
        GameController.Init(player);
        Managers.CameraMgr.InitGameCamera(player);
    }
    public void UpdateMouseScreenPos(InputAction.CallbackContext context)
    {
        MouseScreenPos = context.ReadValue<Vector2>();
    }
    public Vector3Int GetMouseCellPos()
    {
        if (_grid == null) { _grid = Managers.WorldMgr.World_go.GetComponent<Grid>(); }
        return _grid.WorldToCell(GetMouseWorldPos());
    }
    public Vector3 GetMouseWorldPos()
    {
        Vector3 pos = MainCamera.ScreenToWorldPoint(MouseScreenPos);
        pos.z = 0;
        return pos;
    }
    public void Clear()
    {
        _gameInputController.Clear();
        _gameInputSystem = null;
        _gameInputController = null;
        _mainCamera = null;
    }
}
