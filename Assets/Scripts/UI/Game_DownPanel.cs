using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_DownPanel : MonoBehaviour
{
    bool _deactivateAll;
    int _leftIndex;
    int _rightIndex;
    Slot_Skill_DownPanel[] _leftInputHandlerArr;
    Slot_Skill_DownPanel[] _rightInputHandlerArr;

    ISlot_Content[,] _leftSkillSets;
    ISlot_Content[,] _rightSkillSets;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        Transform[] children = gameObject.GetChildren();
        _leftInputHandlerArr = children[0].GetComponentsInChildren<Slot_Skill_DownPanel>();
        _rightInputHandlerArr = children[2].GetComponentsInChildren<Slot_Skill_DownPanel>();
        _leftSkillSets = new ISlot_Content[5, 9];
        _rightSkillSets = new ISlot_Content[5, 9];
        foreach (Slot_Skill_DownPanel handler in _leftInputHandlerArr)
        {
            handler.Init();
        }
        foreach (Slot_Skill_DownPanel handler in _rightInputHandlerArr)
        {
            handler.Init();
        }
    }
    public void UpdateDownPanelContentSet(bool isLeft, int index, ISlot_Content content)
    {
        if (isLeft) 
        {
            _leftSkillSets[_leftIndex, index] = content;
            return;
        }
        _rightSkillSets[_rightIndex, index] = content;

    }
    public void UpdateDownPanelSlots()
    {
        for(int i = 0; i < 9; i++)
        {
            _leftInputHandlerArr[i].UpdateContent(_leftSkillSets[_leftIndex, i]);
            _rightInputHandlerArr[i].UpdateContent(_rightSkillSets[_rightIndex, i]);
        }
        if (_deactivateAll) DeactivateAll();
    }
    public void SwitchLeft(int delta)
    {
        _leftIndex+=delta;
        if(_leftIndex > 4) { _leftIndex -= 5; }
        if(_leftIndex < 0) { _leftIndex += 5; }
        UpdateDownPanelSlots();
    }
    public void SwitchRight(int delta)
    {
        _rightIndex+=delta;
        if (_rightIndex > 4) { _rightIndex -= 5; }
        if (_rightIndex < 0) { _rightIndex += 5; }
        UpdateDownPanelSlots();
    }
    public void PutSkill(ISlot_Content newContent)
    {
        for(int n = 0; n < 5; n++)
        {
            for(int m =0; m < 9; m++)
                {
                if (_leftSkillSets[n, m] == null) 
                { 
                    _leftSkillSets[n,m] = newContent;
                    UpdateDownPanelSlots();
                    return;
                }
            }
            for(int m = 0; m < 9; m++)
            {
                if (_rightSkillSets[n, m] == null)
                {
                    _rightSkillSets[n, m] = newContent;
                    UpdateDownPanelSlots();
                    return;
                }
            }
        }
    }
    public void DeactivateAll()
    {
        _deactivateAll = true;
        for (int i = 0; i < 9; i++)
        {
            _leftInputHandlerArr[i].DisableSkill();
            _rightInputHandlerArr[i].DisableSkill();
        }
    }
    public void ActivateAll()
    {
        _deactivateAll = false;
        UpdateDownPanelSlots();
    }
}
