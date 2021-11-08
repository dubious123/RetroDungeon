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
    public List<Slot> slots;
    public void Init()
    {
        slots = new List<Slot>();    
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
        foreach (Slot slot in slots)
        {
            slot.UpdateSlot();
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
        slots.Add(Managers.ResourceMgr.Instantiate(_SlotPrefab, _SlotGroup.transform).GetComponentInChildren<Slot>(true));
    }
    public Slot GetFirstEmptySlot()
    {
        foreach (Slot slot in slots)
        {
            if (slot.IsEmpty()) return slot;
        }
        return null;
    }
    public void DisableRaycastForAllEmptySlots()
    {
        foreach (Slot slot in slots)
        {
            if (slot.IsEmpty()) slot.DisableRaycast();
        }
    }
    public void EnableAllRaycastForAllSlots()
    {
        foreach (Slot slot in slots)
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
