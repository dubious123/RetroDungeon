using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillIconInputHandler : MonoBehaviour, Imouse
{
    ClickCircleInputHandler _handler;
    Canvas _skillHolder;
    RectTransform _rect;
    Image _this;
    Sprite _temp;
    GUI _gui;
    ActiveSkillCache _activeSkill;
    [SerializeField] string _SkillName;
    float _duration;
    bool _disabled;
    public void Init()
    {
        _rect = GetComponent<RectTransform>();
        _skillHolder = transform.parent.GetComponent<Canvas>();
        _activeSkill = _skillHolder.transform.parent.parent.GetComponent<ActiveSkillCache>();
        _handler = GameObject.Find("MainCircle").GetComponent<ClickCircleInputHandler>();
        _this = transform.GetComponent<Image>();
        _gui = GetComponent<GUI>();
        _gui.enabled = false;
        _SkillName = "MagicArrow";
        _disabled = false;
    }
    public void OnMouseDown(InputAction.CallbackContext context)
    {
        Managers.UI_Mgr.EndDisplayClickCell();
        _handler.Deactivate();
        _duration = 0;
        _skillHolder.sortingOrder = 1;
        _this.raycastTarget = false;
    }
    public void OnMouseUp(InputAction.CallbackContext context)
    {
        if(_activeSkill.Skill == this)
        {
            Cancel();
            Managers.InputMgr.GameController.RightClickEvent.RemoveListener(Cancel);
        }
        else if (_duration < 0.2 && _SkillName != null && _disabled == false) 
        {
            if(_activeSkill.Skill != null)
            {
                Managers.InputMgr.GameController.RightClickEvent.RemoveListener(_activeSkill.Skill.Cancel);
            }
            Managers.GameMgr.Player_Controller.ReachableEmptyTileDict.Clear();
            Managers.GameMgr.Player_Controller.ReachableOccupiedCoorSet.Clear();
            Managers.GameMgr.Player_Controller.ResetSkill();
            Managers.GameMgr.Player_Controller.UpdateSkill(_SkillName);
            Managers.InputMgr.GameController.RightClickEvent.RemoveListener(Cancel);
            Managers.InputMgr.GameController.RightClickEvent.AddListener(Cancel);
            _activeSkill.Skill = this;
        }
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
        _gui.GUIEvent.RemoveListener(UpdatePositionGUI);
        _gui.GUIEvent.AddListener(UpdatePositionGUI);
    }
    public void OnMouseMove(InputAction.CallbackContext context) { }
    public void OnMouseHover(InputAction.CallbackContext context) { }
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
        transform.position = Managers.InputMgr.MouseScreenPos;
    }
    public void Cancel()
    {
        _activeSkill.Skill = null;
        Managers.GameMgr.Player_Controller.ResetSkill();
        Managers.GameMgr.Player_Controller.UpdatePlayerState(Define.UnitState.Idle);
    }
    public void DisableSkill()
    {
        _disabled = true;
        _this.CrossFadeAlpha(0.5f,0.2f,true);
    }
    public void EnableSkill()
    {
        _disabled = false;
        _this.CrossFadeAlpha(1f, 0.2f, true);
    }
}
