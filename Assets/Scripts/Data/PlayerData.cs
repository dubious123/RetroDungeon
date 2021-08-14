using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerData : BaseUnitData
{
    public override void Init()
    {
        base.Init();
        _unitName = "Player";
        _unitType = Define.Unit.Player;
        _currentCellCoor = _floor.WorldToCell(Vector3Int.zero);
        _weapon = Define.WeaponType.None;
        _lookDir = Define.CharDir.Right;
        _maxAp = 7;
        _recoverAp = 5;
        _currentAp = 5;
        _moveSpeed = 3.5f;
    }

}
