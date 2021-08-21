using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillIconInputHandler : MonoBehaviour, Imouse
{
    RectTransform _rect;
    HorizontalLayoutGroup _parentLayout;
    void Awake()
    {
        Init();
    }
    public void Init()
    {
        _rect = GetComponent<RectTransform>();
        _parentLayout = transform.parent.parent.GetComponent<HorizontalLayoutGroup>();
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        
    }

    public void OnMouseDown(InputAction.CallbackContext context)
    {
        Debug.Log("onMouseDown");
        _parentLayout.enabled = false;
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {

    }

    public void OnMouseUp(InputAction.CallbackContext context)
    {
        Debug.Log("OnMouseUp");
        _parentLayout.enabled = true;
    }
    public void OnMouseHover(InputAction.CallbackContext context)
    {

    }
}
