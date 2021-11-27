using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Slot_Skill_DownPanel : Slot, Imouse
{
    ISlot_Content _content;
    ClickCircleInputHandler _handler;
    [SerializeField] ItemCount _ItemCount;
    Canvas _skillHolder;
    RectTransform _rect;
    Image _image;
    GUI _gui;
    ActiveSkillCache _activeSkill;
    bool _isLeft;
    float _duration;
    bool _disabled;
    public void Init()
    {
        _rect = GetComponent<RectTransform>();
        _skillHolder = transform.parent.GetComponent<Canvas>();
        _activeSkill = _skillHolder.transform.parent.parent.GetComponent<ActiveSkillCache>();
        _isLeft = _skillHolder.transform.parent.GetComponent<DownPanel_LeftOrRight>().IsLeft;
        _handler = GameObject.FindWithTag("UI_ClickCircle").GetComponent<ClickCircleInputHandler>();
        _image = transform.GetComponent<Image>();
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
        _image.raycastTarget = false;
    }
    public void OnMouseUp(InputAction.CallbackContext context)
    {
        if(_activeSkill.Skill == this)
        {
            Cancel();
            Managers.InputMgr.GameController.RightClickEvent.RemoveListener(Cancel);
        }
        else if (_duration < 0.2  && _content != null && _disabled == false) 
        {
            if(_activeSkill.Skill != null)
            {
                Managers.InputMgr.GameController.RightClickEvent.RemoveListener(_activeSkill.Skill.Cancel);
            }
            Managers.GameMgr.Player_Controller.ReachableEmptyTileDict.Clear();
            Managers.GameMgr.Player_Controller.ReachableOccupiedCoorSet.Clear();
            Managers.GameMgr.Player_Controller.ResetSkill();
            Managers.GameMgr.Player_Controller.UpdateSkill(_content);
            Managers.InputMgr.GameController.RightClickEvent.RemoveListener(Cancel);
            Managers.InputMgr.GameController.RightClickEvent.AddListener(Cancel);
            _activeSkill.Skill = this;
        }
        _skillHolder.sortingOrder = 0;
        DropDown();
        _image.raycastTarget = true;
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
        Slot slot = obj.GetComponent<Slot>();
        ISlot_Content temp = _content;
        UpdateContent(slot?.GetContent());
        if (slot is Slot_Item) return;
        slot?.UpdateContent(temp);
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
        _image.CrossFadeAlpha(0.5f,0.2f,true);
    }
    public void EnableSkill()
    {
        _disabled = false;
        _image.CrossFadeAlpha(1f, 0.2f, true);
    }
    public override ISlot_Content GetContent()
    {
        return _content;
    }
    public override void UpdateContent(ISlot_Content content)
    {
        if (content == null)
        {
            DisableSkill();
            _image.sprite = null;
            _ItemCount.gameObject.SetActive(false);
        }
        else if (content is BaseSkill skill)
        {
            if (skill.Cost > Managers.GameMgr.Me.Stat.Ap) { DisableSkill(); }
            else EnableSkill();
            _image.sprite = Managers.ResourceMgr.GetSkillSprite(skill.Name);
            _ItemCount.gameObject.SetActive(false);
            _content = skill;
        }
        else if (content is BaseItem item)
        {
            if (!item.Usable) return;
            if (item.ItemUseContent.Cost > Managers.GameMgr.Me.Stat.Ap || item.CurrentStack <= 0) { DisableSkill(); }
            else EnableSkill();
            _image.sprite = Managers.ResourceMgr.GetItemSprite(item.ItemName);
            _ItemCount.gameObject.SetActive(true);
            _ItemCount.UpdateCount(item.CurrentStack);
            _content = item;
        }
        else { throw new System.Exception(); }
        if (Managers.UI_Mgr._Popup_PlayerInfo.gameObject.activeInHierarchy) DisableSkill();
        Managers.UI_Mgr.DownPanel.UpdateDownPanelContentSet(_isLeft, transform.parent.GetSiblingIndex(), _content);
    }
    public override void DeleteContent()
    {
        _image.sprite = null;
        DisableSkill();
    }
    public override bool IsEmpty()
    {
        return _content is null;
    }

    public void OnMouseRightClick(InputAction.CallbackContext context)
    {
        if (IsEmpty()) return;
    }
}
