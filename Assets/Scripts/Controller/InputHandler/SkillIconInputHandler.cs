using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillIconInputHandler : MonoBehaviour, Imouse
{
    Canvas _skillHolder;
    RectTransform _rect;
    Image _this;
    Sprite _temp;
    GUI _gui;
    string _skillName;
    float _duration;

    void Awake()
    {
        Init();
    }
    public void Init()
    {
        _rect = GetComponent<RectTransform>();
        _skillHolder = transform.parent.GetComponent<Canvas>();
        _this = transform.GetComponent<Image>();
        _gui = GetComponent<GUI>();
        _gui.enabled = false;
        _skillName = "Blunt";
    }
    public void OnMouseDown(InputAction.CallbackContext context)
    {
        _duration = 0;
        _skillHolder.sortingOrder = 1;
        _this.raycastTarget = false;
    }
    public void OnMouseUp(InputAction.CallbackContext context)
    {
        if (_duration < 0.2 && _skillName != null) { Managers.GameMgr.Player_Controller.UpdateSkill(_skillName); }
        _skillHolder.sortingOrder = 0;
        DropDown();
        _this.raycastTarget = true;
        _rect.anchoredPosition = Vector2.zero;
        _gui.GUIEvent.RemoveListener(UpdatePositionGUI);
        _gui.enabled = false;
    }
    public void OnDrag(InputAction.CallbackContext context)
    {
        _duration += Time.deltaTime;
        if(_duration < 0.2) { return; }
        _gui.enabled = true;
        _gui.GUIEvent.AddListener(UpdatePositionGUI);
    }
    public void OnMouseMove(InputAction.CallbackContext context) { }
    public void OnMouseHover(InputAction.CallbackContext context)
    {
    }
    public void DropDown()
    {
        Managers.InputMgr.GameController.HoverTarget?.GetDrop(gameObject);
    }
    public void GetDrop(GameObject obj)
    {
        Image other = obj.GetComponent<SkillIconInputHandler>()?.GetComponent<Image>();
        if(other == null) { return; }
        _temp = _this.sprite;
        _this.sprite = other.sprite;
        other.sprite = _temp;
    }
    private void UpdatePositionGUI()
    {
        transform.position = Managers.InputMgr.MouseScreenPosition;
    }
}
