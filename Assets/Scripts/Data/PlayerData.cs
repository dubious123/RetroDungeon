using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerData : UnitData
{
    public override void Init()
    {
        base.Init();

        _currentCellCoor = _floor.WorldToCell(Vector3Int.zero);
        _weapon = Define.WeaponType.None;
        _lookDir = Define.CharDir.Right;
        _maxAp = 7;
        _recoverAp = 5;
        _currentAp = 5;
        _moveSpeed = 3.5f;
    }

    internal int UpdateAp(int cost)
    {
        _currentAp += cost;
        if(_currentAp > _maxAp) { _currentAp = _maxAp; }
        else if(_currentAp < 0) { _currentAp = 0; }
        return _currentAp;
    }
}
