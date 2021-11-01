using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillIconInputHandler : MonoBehaviour, Imouse
{
    ClickCircleInputHandler _handler;
    [SerializeField] ItemCount _ItemCount;
    Canvas _skillHolder;
    RectTransform _rect;
    Image _this;
    Sprite _temp;
    GUI _gui;
    ActiveSkillCache _activeSkill;
    bool _isLeft;
    string _skillName;
    float _duration;
    bool _disabled;
    public void Init()
    {
        _rect = GetComponent<RectTransform>();
        _skillHolder = transform.parent.GetComponent<Canvas>();
        _activeSkill = _skillHolder.transform.parent.parent.GetComponent<ActiveSkillCache>();
        _isLeft = _skillHolder.transform.parent.GetComponent<DownPanel_LeftOrRight>().IsLeft;
        _handler = GameObject.FindWithTag("UI_ClickCircle").GetComponent<ClickCircleInputHandler>();
        _this = transform.GetComponent<Image>();
        _gui = GetComponent<GUI>();
        _gui.enabled = false;
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
        else if (_duration < 0.2  && _skillName != null && _disabled == false) 
        {
            if(_activeSkill.Skill != null)
            {
                Managers.InputMgr.GameController.RightClickEvent.RemoveListener(_activeSkill.Skill.Cancel);
            }
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
        SkillIconInputHandler other = obj.GetComponent<SkillIconInputHandler>();
        if(other == null) { return; }
        string temp = _skillName;
        _skillName = other._skillName;
        other._skillName = temp;
        Managers.UI_Mgr.DownPanel.UpdateSkill(_isLeft, transform.parent.GetSiblingIndex(), _skillName);
        Managers.UI_Mgr.DownPanel.UpdateSkill(other._isLeft, other.transform.parent.GetSiblingIndex(), other._skillName);
        Managers.UI_Mgr.DownPanel.UpdateSkillIcons();
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
    public void UpdateSkill(string skillName)
    {
        _skillName = skillName;
        if(_skillName == null) 
        {
            DisableSkill();
            _this.sprite = null;
            _ItemCount.gameObject.SetActive(false);
            return;
        }
        if(Managers.GameMgr.Me.SkillDict.TryGetValue(skillName,out BaseSkill skill))
        {
            if (skill.Cost > Managers.GameMgr.Me.Stat.Ap) { DisableSkill(); }
            else EnableSkill();
            _this.sprite = Managers.ResourceMgr.GetSkillSprite(skillName);
            _ItemCount.gameObject.SetActive(false);
            return;
        }
        else if(Managers.GameMgr.Me.ItemDict.TryGetValue(skillName, out BaseItem item))
        {
            if (item.ItemUseContent.Cost > Managers.GameMgr.Me.Stat.Ap || item.CurrentStack <= 0) { DisableSkill(); }
            else EnableSkill();
            _this.sprite = Managers.ResourceMgr.GetSkillSprite(item.ItemUseContent.Name);
            _ItemCount.gameObject.SetActive(true);
            _ItemCount.Count.text = item.CurrentStack.ToString();
            return;
        }
        else { throw new System.Exception(); }
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
