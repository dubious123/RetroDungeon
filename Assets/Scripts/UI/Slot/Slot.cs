using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] GameObject _Content_go;
    [SerializeField] Image _SlotImage;
    [SerializeField] Image _ContentImage;
    ISlot_Content _content;
    ISlot_Content Content
    {
        get 
        { 
            if(_content == null)
            {
                _content = _Content_go.GetComponent<ISlot_Content>();
            }
            return _content; 
        }
    }
    public void Init()
    {

    }
    public void UpdateSlot()
    {
        Content.UpdateContent();
    }
    public void UpdateSlot(object obj)
    {
        Content.UpdateContent(obj);
    }
    public T GetContent<T>(object obj) 
    {
        Content.GetContent(ref obj);
        if(obj is T) { return (T)obj; }
        return default(T);
    }
    public void PutContent(object obj)
    {
        Content.UpdateContent(obj);
    }
    public void DeleteContent()
    {
        Content.DeleteContent();
    }
    public bool IsEmpty()
    {
        return Content.IsEmpty();
    }
    public void DisableRaycast()
    {
        _SlotImage.raycastTarget = false;
        _ContentImage.raycastTarget = false;
    }
    public void EnableRaycast()
    {
        _SlotImage.raycastTarget = true;
        _ContentImage.raycastTarget = true;
    }





}
