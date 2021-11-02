using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Slot : MonoBehaviour, Imouse
{
    [SerializeField] UnityEvent _GetDropEvent;
    [SerializeField] UnityEvent _OnMouseDownEvent;
    [SerializeField] UnityEvent _OnMouseUpEvent;
    [SerializeField] Image _Image;
    BaseItem _item;
    public BaseItem Item { get { return _item; } }
    public void Init()
    {

    }
    public void GetDrop(GameObject obj)
    {
        _GetDropEvent.Invoke();
    }
    public void OnMouseDown(InputAction.CallbackContext context)
    {
        _OnMouseDownEvent.Invoke();
    }
    public void OnMouseUp(InputAction.CallbackContext context)
    {
        _OnMouseUpEvent.Invoke();
    }
    public void PutContent(BaseItem item)
    {
        _Image.sprite = Managers.ResourceMgr.GetItemSprite(item);
        _item = item;
        _Image.ChangeAlpha(1f);
    }
    public void DeleteContent()
    {
        _Image.sprite = null;
        _item = null;
        _Image.ChangeAlpha(0f);
    }
    public bool IsEmpty()
    {
        return _item == null;
    }



    public void OnDrag(InputAction.CallbackContext context) { }
    public void OnMouseMove(InputAction.CallbackContext context) { }
    public void OnMouseHover(InputAction.CallbackContext context) { }
    public void DropDown() { }


}
