using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class InputManager
{
    MouseInput MouseAction;
    Camera _mainCamera;
    Tilemap _map;
    Vector2 _mouseScreenPosition;
    Vector3 _mouseWorldPosition;
    Vector3Int _clickedGridCellPosition;
    Vector3 destination;

    public Vector2 MouseScreenPosition { get { return _mouseScreenPosition; } }
    public Vector3 MouseWorldPosition { get { return _mouseWorldPosition; } }
    public Vector3Int ClickedGridCellPosition { get { return _clickedGridCellPosition; } }
    public void Init()
    {
        _mainCamera = Camera.main;
    }
    public Vector3Int? GetClickedCellPosition(Vector2 screenMousePos)
    {
        _map = Managers.GameMgr.Floor;
        //Todo, need new func
        _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(screenMousePos);
        _mouseWorldPosition.z = 0;  

        _clickedGridCellPosition = Managers.DungeonMgr.CurrentDungeon.GetComponent<Grid>().WorldToCell(_mouseWorldPosition);
        if (_map.HasTile(_clickedGridCellPosition))
        {
            Debug.Log("Yes");
            Debug.Log(_clickedGridCellPosition);
            return _clickedGridCellPosition;
        }
        return null;
    }
    
}
