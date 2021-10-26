using MEC;
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

        UnitName = "Player";
        UnitType = Define.Unit.Player;
        Weapon = Define.WeaponType.None;
    }
    public override bool IsDead()
    {
        //Debug.Log("checking dead -- player");
        bool isPlayerDead = false;
        return isPlayerDead || base.IsDead();
    }
    public override IEnumerator<float> _PerformDeath()
    {
        base._PerformDeath().RunCoroutine();
        GetComponent<PlayerController>()._HandleDie().CancelWith(gameObject).RunCoroutine();
        yield break;
    }
    public override void Response()
    {
        base.Response();
    }
}
