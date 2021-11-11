using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Slot_Equipment : Slot, Imouse
{
    BaseItem _equipment;
    [SerializeField] Image _EquipmentImage;
    public override void UpdateContent(ISlot_Content content)
    {
        if (content is null) return;
        if (!(content is BaseItem temp)) return;
        if (temp.Wearable) _equipment = temp;
        Managers.GameMgr.Player_Data.ApplyEquipmentStat(_equipment);
        _EquipmentImage.sprite = Managers.ResourceMgr.GetItemSprite(_equipment);
    }
    public override void DeleteContent()
    {
    }

    public void DropDown()
    {
    }

    public override ISlot_Content GetContent()
    {
        return _equipment;
    }

    public void GetDrop(GameObject obj)
    {
        Slot slot = obj.GetComponent<Slot>();
        ISlot_Content other = slot.GetContent();
        if (other == null) return;
        if (other is BaseItem temp && temp.Wearable)
        {         
            slot.UpdateContent(temp);
        }
    }

    public void Init()
    {
    }

    public override bool IsEmpty()
    {
        return _equipment == null;
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
    }

    public void OnMouseDown(InputAction.CallbackContext context)
    {
    }

    public void OnMouseHover(InputAction.CallbackContext context)
    {
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
    }

    public void OnMouseUp(InputAction.CallbackContext context)
    {
    }


}
