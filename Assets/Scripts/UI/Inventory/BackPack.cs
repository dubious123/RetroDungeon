using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPack : MonoBehaviour
{
    [SerializeField] GameObject _SlotPrefab;
    [SerializeField] GridLayoutGroup _SlotGroup;
    [SerializeField] ScrollRect _ScrollRect;
    public bool IsFull { get; set; }
    public List<Slot_Item> slots;
    public void Init()
    {
        slots = new List<Slot_Item>();    
        for (int i = 0; i < 28; i++)
        {
            AddNewSlot();
        }
    }
    public void OnEnable()
    {
        UpdateBackPack();
    }
    public void UpdateBackPack()
    {
        foreach (Slot_Item slot in slots)
        {
            slot.UpdateContent();
        }
    }
    public void AddNewRow()
    {
        for(int i = 0; i< _SlotGroup.Size().x; i++)
        {
            AddNewSlot();
        }
    }
    public void AddNewSlot()
    {
        slots.Add(Managers.ResourceMgr.Instantiate(_SlotPrefab, _SlotGroup.transform).GetComponentInChildren<Slot_Item>(true));
    }
    public Slot_Item GetFirstEmptySlot()
    {
        foreach (Slot_Item slot in slots)
        {
            if (slot.IsEmpty()) return slot;
        }
        return null;
    }
    public void DisableRaycastForAllEmptySlots()
    {
        foreach (Slot_Item slot in slots)
        {
            if (slot.IsEmpty()) slot.DisableRaycast();
        }
    }
    public void EnableAllRaycastForAllSlots()
    {
        foreach (Slot_Item slot in slots)
        {
            slot.EnableRaycast();
        }
    }
    public void StopMoving()
    {
        _ScrollRect.vertical = false;
    }
    public void ResumeMoving()
    {
        _ScrollRect.vertical = true;
    }
}
