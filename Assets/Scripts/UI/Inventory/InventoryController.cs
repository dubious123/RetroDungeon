using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class InventoryController : MonoBehaviour
{
    public Equipment _Equipment;
    public BackPack _BackPack;
    PlayerData _player;
    public void Init()
    {
        _player = Managers.GameMgr.Player_Data;
        _Equipment.Init();
        _BackPack.Init();

    }

    public void AddItem(BaseItem item)
    {
        if (_BackPack.IsFull)
        {
            _BackPack.AddNewRow();
        }
        _BackPack.GetFirstEmptySlot().UpdateContent(item);
    }

    internal void EquipItem(BaseItem item)
    {
        switch (item.Equipment_Type)
        {
            case EquipmentType.Helmet:
                _player.EquipmentDict[EquipmentType.Helmet] = item;
                _Equipment.WearHelmet(item);
                break;
            case EquipmentType.Armor:
                _player.EquipmentDict[EquipmentType.Armor] = item;
                _Equipment.WearArmor(item);
                break;
            case EquipmentType.Weapon:
                _player.EquipmentDict[EquipmentType.Weapon] = item;
                _Equipment.WearWeapon(item);
                break;
            case EquipmentType.Boot:
                _player.EquipmentDict[EquipmentType.Boot] = item;
                _Equipment.WearBoot(item);
                break;
        }

    }
}
