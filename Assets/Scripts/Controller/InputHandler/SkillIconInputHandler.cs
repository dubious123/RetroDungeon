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
    ActiveSkillCache _activeSkill;
    string _skillName;
    float _duration;
    bool _disabled;
    void Awake()
    {
        Init();
    }
    public void Init()
    {
        _rect = GetComponent<RectTransform>();
        _skillHolder = transform.parent.GetComponent<Canvas>();
        _activeSkill = _skillHolder.transform.parent.parent.GetComponent<ActiveSkillCache>();
        _this = transform.GetComponent<Image>();
        _gui = GetComponent<GUI>();
        _gui.enabled = false;
        _skillName = "Blunt";
        _disabled = false;
    }
    public void OnMouseDown(InputAction.CallbackContext context)
    {
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
        else if(_activeSkill.Skill != null)
        {
            Managers.InputMgr.GameController.RightClickEvent.RemoveListener(_activeSkill.Skill.Cancel);
        }
        else if (_duration < 0.2 && _skillName != null && _disabled == false) 
        {
            Managers.GameMgr.Player_Controller.ResetReachableTiles();
            Managers.GameMgr.Player_Controller.ReachableEmptyTileDict.Clear();
            Managers.GameMgr.Player_Controller.ReachableOccupiedCoorSet.Clear();
            Managers.GameMgr.Player_Controller.ResetSkill();
            Managers.GameMgr.Player_Controller.UpdateSkill(_skillName);
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
        transform.position = Managers.InputMgr.MouseScreenPosition;
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
