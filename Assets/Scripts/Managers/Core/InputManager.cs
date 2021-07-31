using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputManager
{
    MouseInput mouseInput;
    Camera mainCamara;
    Tilemap _map;
    Vector2 _mouseScreenPosition;
    Vector3 _mouseWorldPosition;
    Vector3Int _gridPosition;
    Vector3 destination;

    public Vector2 MouseScreenPosition { get { return _mouseScreenPosition; } }
    public Vector3 MouseWorldPosition { get { return _mouseWorldPosition; } }
    public Vector3Int GridPosition { get { return _gridPosition; } }
    public void Init()
    {
        mouseInput = new MouseInput();
        mouseInput.Enable();
        mainCamara = Camera.main;
        _map = Managers.Game.Ground;
        mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
    }
    public void OnUpdate()
    {
        _mouseScreenPosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        _mouseWorldPosition = mainCamara.ScreenToWorldPoint(_mouseScreenPosition);
    }
    private void MouseClick()
    {
        _map = Managers.Game.Ground;
        GetClickedCellGridPosition();
    }

    public Vector3Int GetClickedCellGridPosition()
    {
        _gridPosition = _map.WorldToCell(_mouseWorldPosition);
        Debug.Log(_gridPosition);
        return _gridPosition;
    }


}
