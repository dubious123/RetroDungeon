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

    public PlayerInput GameInputSystem { get { return _gameInputSystem; } }
    public GameInputController GameController { get { return _gameInputController; } }
    public Vector2 MouseScreenPosition { get { return _mouseScreenPosition; } set { _mouseScreenPosition = value; } }
    public Vector3 MouseWorldPosition { get { return _mouseWorldPosition; } }
    public void Init()
    {
        _gameInputController = Managers.ResourceMgr.Instantiate("GameInputController").GetComponent<GameInputController>();
        _gameInputSystem = _gameInputController.GetComponent<PlayerInput>();
        _mainCamera = Camera.main;
        
    }
    public void InitControllers(GameObject player)
    {
        _gameInputController.Init(player);
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
        _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(screenMousePos);
        _mouseWorldPosition.z = 0;  

        _currentMouseCellPos = _grid.WorldToCell(_mouseWorldPosition);
        if (_map.HasTile(_currentMouseCellPos))
        {
            return _currentMouseCellPos;
        }
        return null;
    }
    
}
