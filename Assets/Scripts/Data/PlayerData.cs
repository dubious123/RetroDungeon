using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerData
{
    Tilemap _floor;
    Vector3Int _currentCellCoor;
    Define.CharDir _lookDir;
    float _moveSpeed;
    int _maxAp;
    int _currentAp;
    Define.WeaponType _weapon;
    public Vector3Int CurrentCellCoor { get { return _currentCellCoor; } }
    public Define.CharDir LookDir { get { return _lookDir; }set { _lookDir = value; } }
    public float Movespeed { get { return _moveSpeed; } }
    public int MaxAp { get { return _maxAp; } }
    public int CurrentAp { get { return _maxAp; } }
    public Define.WeaponType Weapon { get { return _weapon; } }
    public PlayerData(GameObject player,Transform parent)
    {
        Init(player,parent);
    }
    void Init(GameObject player, Transform parent)
    {
        _floor = parent.GetComponent<Tilemap>();
        _currentCellCoor = _floor.WorldToCell(Vector3Int.zero);
        _weapon = Define.WeaponType.None;
        _lookDir = Define.CharDir.Right;
        _maxAp = 5;
        _moveSpeed = 3.5f;
    }
}
