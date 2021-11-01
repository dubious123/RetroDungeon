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
    void Start()
    {
        _playerData = Managers.GameMgr.Player_Data;
    }

    void Update()
    {
        
    }
    void Wear(BaseItem item, Slot slot)
    {
        slot.PutContent(item);
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
