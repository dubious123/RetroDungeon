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
    Tilemap _map;
    Vector2 _mouseScreenPosition;
    Vector3 _mouseWorldPosition;
    Vector3Int _currentMouseCellPos;

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
    public Vector2 MouseScreenPosition { get { return _mouseScreenPosition; } set { _mouseScreenPosition = value; } }
    public Vector3 MouseWorldPosition { get { return _mouseWorldPosition; } }
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
        _mouseScreenPosition = context.ReadValue<Vector2>();
    }
    public Vector3Int? GetMouseCellPos(Vector2 screenMousePos)
    {
        if(_grid == null) { _grid = Managers.DungeonMgr.CurrentDungeon.GetComponent<Grid>(); }
        _map = Managers.GameMgr.Floor;
        
        //Todo, need new func
        _mouseWorldPosition = MainCamera.ScreenToWorldPoint(screenMousePos);
        _mouseWorldPosition.z = 0;  

        _currentMouseCellPos = _grid.WorldToCell(_mouseWorldPosition);
        if (_map.HasTile(_currentMouseCellPos))
        {
            return _currentMouseCellPos;
        }
        return null;
    }
    public void Clear()
    {
        _gameInputController.Clear();
        _gameInputSystem = null;
        _gameInputController = null;
        _mainCamera = null;
        _map = null;
    }
}
