using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class InputManager
{
    MouseInput MouseAction;
    Camera _mainCamera;
    Tilemap _map;
    Vector2 _mouseScreenPosition;
    Vector3 _mouseWorldPosition;
    Vector3Int _CurrentMouseCellPosition;
    Vector3 destination;

    public Vector2 MouseScreenPosition { get { return _mouseScreenPosition; } }
    public Vector3 MouseWorldPosition { get { return _mouseWorldPosition; } }
    public Vector3Int ClickedGridCellPosition { get { return _CurrentMouseCellPosition; } }
    public void Init()
    {
        _mainCamera = Camera.main;
    }
    public void SetCameraInputSystem(GameObject player)
    {
        PlayerInput input = player.GetComponent<PlayerInput>();
        input.currentActionMap.FindAction("Camera2DMovement").performed += Managers.CameraMgr.GameCam.UpdateManualPosition;
        input.currentActionMap.FindAction("Camera2DMovement").canceled += Managers.CameraMgr.GameCam.UpdateManualPosition;
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
