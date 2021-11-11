using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Slot Helmet;
    public Slot Armor;
    public Slot Weapon;
    public Slot Boot;
    PlayerData _playerData;
    public void Init()
    {
        _playerData = Managers.GameMgr.Player_Data;

    }
    void Wear(BaseItem item, Slot slot)
    {
        slot.UpdateContent(item);
    }
    public void WearHelmet(BaseItem item)
    {
        Wear(item, Helmet);
    }
    public void WearArmor(BaseItem item)
    {
        Wear(item, Armor);
    }
    public void WearWeapon(BaseItem item)
    {
        Wear(item, Weapon);
    }
    public void WearBoot(BaseItem item)
    {
        Wear(item, Boot);
    }
}
