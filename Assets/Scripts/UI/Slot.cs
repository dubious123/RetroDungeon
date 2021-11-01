using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Slot : MonoBehaviour, Imouse
{
    [SerializeField] UnityEvent _GetDropEvent;
    [SerializeField] UnityEvent _OnMouseDownEvent;
    [SerializeField] UnityEvent _OnMouseUpEvent;
    BaseItem item;
    void Start()
    {

    }
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

    }




    public void OnDrag(InputAction.CallbackContext context) { }
    public void OnMouseMove(InputAction.CallbackContext context) { }
    public void OnMouseHover(InputAction.CallbackContext context) { }
    public void DropDown() { }


}
