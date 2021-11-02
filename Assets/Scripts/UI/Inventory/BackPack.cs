using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPack : MonoBehaviour
{
    [SerializeField] GameObject _SlotPrefab;
    [SerializeField] GridLayoutGroup _SlotGroup;
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
    public void AddNewRow()
    {
        for(int i = 0; i< _SlotGroup.Size().x; i++)
        {
            AddNewSlot();
        }
    }
    public void AddNewSlot()
    {
        slots.Add(Managers.ResourceMgr.Instantiate(_SlotPrefab, _SlotGroup.transform).GetComponent<Slot>());
    }
    public Slot GetFirstEmptySlot()
    {
        foreach (Slot slot in slots)
        {
            if (slot.IsEmpty()) return slot;
        }
        return null;
    }
}
