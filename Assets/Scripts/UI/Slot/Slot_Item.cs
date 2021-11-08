using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Slot_Item : MonoBehaviour ,ISlot_Content
{
    BaseItem _item;
    public BaseItem Item { get { return _item; } }
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
    public void GetContent(ref object obj)
    {
        if (!(obj is BaseItem)) obj = null;
        else obj = _item;
    }
    public void UpdateContent()
    {
        if (_item == null || _item.CurrentStack <= 0) DeleteContent();
        else UpdateContent(_item);
    }
    public void UpdateContent(object item)
    {
        _item = item as BaseItem;
        if (item == null)
        {
            DeleteContent();
            return;
        }
        _Image.sprite = Managers.ResourceMgr.GetItemSprite(_item);
        _Image.ChangeAlpha(1f);
        _Count.gameObject.SetActive(true);
        _Count.UpdateCount(_item.CurrentStack);
    }
    public void DeleteContent()
    {
        _Image.sprite = null;
        _item = null;
        _Image.ChangeAlpha(0f);
        _Count.gameObject.SetActive(false);
    }

    public bool IsEmpty()
    {
        return _item == null;
    }

    public void Init()
    {
    }

    public void OnMouseDown(InputAction.CallbackContext context)
    {
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
        BaseItem other = new BaseItem();
        other = slot.GetContent<BaseItem>(other);
        if (other == null) { return; }
        BaseItem temp = _item;
        UpdateContent(other);
        slot.UpdateSlot(temp);
    }
}
