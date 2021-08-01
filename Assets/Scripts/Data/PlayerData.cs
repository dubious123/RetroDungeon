using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerData
{
    Vector3Int _currentCellCoor;
    Vector3Int _currentCartCoor;
    Tilemap _floor;
    float _moveSpeed;
    int _ap;
    public Vector3Int CurrentCellCoor { get { return _currentCellCoor; } }
    public Vector3Int CurrentCartCoor { get { return _currentCartCoor; } }
    public float Movespeed { get { return _moveSpeed; } }
    public int Ap { get { return _ap; } }

    public PlayerData(GameObject player,Transform parent)
    {
        Init(player,parent);
    }
    void Init(GameObject player, Transform parent)
    {
        _floor = parent.GetComponent<Tilemap>();
        _currentCartCoor = Vector3Int.zero;
        _currentCellCoor = _floor.WorldToCell(_currentCartCoor);
        player.transform.position = _floor.GetCellCenterWorld(_currentCellCoor);
        _ap = 5;
        _moveSpeed = 1.0f;
    }
}
