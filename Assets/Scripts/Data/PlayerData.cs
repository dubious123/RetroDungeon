using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerData
{
    Vector3Int _currentCellCoor;
    Tilemap _floor;
    float _moveSpeed;
    int _maxAp;
    int _currentAp;
    public Vector3Int CurrentCellCoor { get { return _currentCellCoor; } }
    public float Movespeed { get { return _moveSpeed; } }
    public int MaxAp { get { return _maxAp; } }
    public int CurrentAp { get { return _maxAp; } }
    public PlayerData(GameObject player,Transform parent)
    {
        Init(player,parent);
    }
    void Init(GameObject player, Transform parent)
    {
        _floor = parent.GetComponent<Tilemap>();
        _currentCellCoor = _floor.WorldToCell(Vector3Int.zero);
        _maxAp = 5;
        _moveSpeed = 0.5f;
    }
}
