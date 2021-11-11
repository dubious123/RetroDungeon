using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;

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
    public override void LearnSkill(BaseSkill skill)
    {
        base.LearnSkill(skill);
        Managers.UI_Mgr.DownPanel.PutSkill(skill);
    }
    public override void PutPocket(BaseItem item)
    {
        base.PutPocket(item);   
    }
    public override void EquipItem(BaseItem item)
    {
        if (_itemDict.ContainsKey(item.ItemName) || item.Wearable)
        {
            ApplyEquipmentStat(item);
            Managers.UI_Mgr._Popup_PlayerInfo.Inventory.EquipItem(item);

        }
        
    }

}
