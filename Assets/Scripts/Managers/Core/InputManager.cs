using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class InputManager
{
    PlayerInput _gameInputSystem;
    GameInputController _gameInputController;
    Camera _mainCamera;
    Tilemap _map;
    Vector2 _mouseScreenPosition;
    Vector3 _mouseWorldPosition;
    Vector3Int _CurrentMouseCellPosition;
    Vector3 destination;

    public PlayerInput GameInputSystem { get { return _gameInputSystem; } }
    public GameInputController GameInputController { get { return _gameInputController; } }
    public Vector2 MouseScreenPosition { get { return _mouseScreenPosition; } }
    public Vector3 MouseWorldPosition { get { return _mouseWorldPosition; } }
    public Vector3Int ClickedGridCellPosition { get { return _CurrentMouseCellPosition; } }
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
    public Vector3Int? UpdateMouseCellPos(Vector2 screenMousePos)
    {
        _map = Managers.GameMgr.Floor;
        
        //Todo, need new func
        _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(screenMousePos);
        _mouseWorldPosition.z = 0;  

        _CurrentMouseCellPosition = Managers.DungeonMgr.CurrentDungeon.GetComponent<Grid>().WorldToCell(_mouseWorldPosition);
        if (_map.HasTile(_CurrentMouseCellPosition))
        {
            return _CurrentMouseCellPosition;
        }
        return null;
    }
    
}
