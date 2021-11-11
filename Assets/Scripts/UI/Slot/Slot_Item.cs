using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Slot_Item : Slot, Imouse
{
    BaseItem _item;
    public BaseItem Item { get { return _item; } }
    [SerializeField] Image _HolderImage;
    [SerializeField] Image _Image;
    [SerializeField] ItemCount _Count;
    [SerializeField] GUI _Gui;
    [SerializeField] RectTransform _Rect;
    [SerializeField] Canvas _ItemHolder;
    BackPack _backpack;

    float _duration;
    public void Start()
    {
        _backpack = Managers.UI_Mgr._Popup_PlayerInfo.Inventory._BackPack;
    }
    public override ISlot_Content GetContent()
    {
        return _item;
    }
    public void UpdateContent()
    {
        if (_item is null) {DeleteContent(); return;}
        _Count.gameObject.SetActive(true);
        _Count.UpdateCount(_item.CurrentStack);
    }
    public override void UpdateContent(ISlot_Content item)
    {
        if (item == null)
        {
            DeleteContent();
            return;
        }
        if (!(item is BaseItem temp))
        {
            return;
        }
        _item = temp;
        _Image.sprite = Managers.ResourceMgr.GetItemSprite(_item);
        _Image.ChangeAlpha(1f);
        _Count.gameObject.SetActive(true);
        _Count.UpdateCount(_item.CurrentStack);
    }
    public override void DeleteContent()
    {
        _Image.sprite = null;
        _item = null;
        _Image.ChangeAlpha(0f);
        _Count.gameObject.SetActive(false);
    }

    public override bool IsEmpty()
    {
        return _item is null;
    }

    public void Init()
    {
    }

    public void OnMouseDown(InputAction.CallbackContext context)
    {
        _duration = 0;
        _ItemHolder.overrideSorting = true;
        _ItemHolder.sortingOrder = 1;
        _backpack.EnableAllRaycastForAllSlots();
        _Image.raycastTarget = false;
    }

    public void OnMouseUp(InputAction.CallbackContext context)
    {
        _ItemHolder.overrideSorting = false;
        _ItemHolder.sortingOrder = 0;
        DropDown();
        _Image.raycastTarget = true;
        _Gui.GUIEvent.RemoveListener(UpdatePositionGUI);
        _Gui.enabled = false;
        _Rect.anchoredPosition = Vector2.zero;
        _backpack.ResumeMoving();
        _backpack.DisableRaycastForAllEmptySlots();

    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        _duration += Time.deltaTime;
        if (_duration < 0.2) { return; }
        _backpack.StopMoving();
        _Gui.enabled = true;
        _Gui.GUIEvent.RemoveListener(UpdatePositionGUI);
        _Gui.GUIEvent.AddListener(UpdatePositionGUI);
    }
    private void UpdatePositionGUI()
    {
        transform.position = Managers.InputMgr.MouseScreenPos;
    }
    public void OnMouseMove(InputAction.CallbackContext context)
    {

    }
    public void OnMouseHover(InputAction.CallbackContext context)
    {
    }

    public void DropDown()
    {
        Managers.InputMgr.GameController.HoverTarget?.GetDrop(gameObject);
    }

    public void GetDrop(GameObject obj)
    {
        Slot slot = obj.GetComponent<Slot>();
        ISlot_Content other = slot.GetContent();
        if (other == null) return;
        if (other is BaseItem) 
        {
            BaseItem temp = _item;
            UpdateContent(other);
            slot.UpdateContent(temp);
        }
        
    }
    public void DisableRaycast()
    {
        _HolderImage.raycastTarget = false;
        _Image.raycastTarget = false;
    }
    public void EnableRaycast()
    {
        _HolderImage.raycastTarget = true;
        _Image.raycastTarget = true;
    }
}
