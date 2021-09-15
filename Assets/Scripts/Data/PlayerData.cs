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
        _allienceList = new List<string>();
        _enemyList = new List<string>();
        _unitName = "Player";
        _unitType = Define.Unit.Player;
        CurrentCellCoor = _floor.WorldToCell(Vector3Int.zero);
        _weapon = Define.WeaponType.None;
        _lookDir = Define.CharDir.Right;

        _maxHp = 100;
        _hp = 100;
        _maxDef = 10;
        _def = 10;
        _maxMp = 50;
        _mp = 50;
        _maxMs = 10;
        _ms = 10;
        _maxShock = 100;
        _shock = 0;
        _maxStress = 100;
        _stress = 0;

        _maxAp = 7;
        _recoverAp = 5;
        _currentAp = 5;
        _moveSpeed = 6.0f;
        #region test
        _skillDict.Add("Blunt", SkillLibrary.GetSkill("Blunt"));
        #endregion
    }
    public override bool IsDead()
    {
        //Debug.Log("checking dead -- player");
        bool isPlayerDead = false;
        return isPlayerDead || base.IsDead();
    }
    public override void PerformDeath()
    {
        Debug.Log("Performing Death -- player");
    }
    public override void Response()
    {
        Debug.Log("Performing Response -- player");
    }
}
