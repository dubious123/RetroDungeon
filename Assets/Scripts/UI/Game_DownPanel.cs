using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_DownPanel : MonoBehaviour
{
    int _leftIndex;
    int _rightIndex;
    SkillIconInputHandler[] _leftInputHandlerArr;
    SkillIconInputHandler[] _rightInputHandlerArr;
    string[,] _leftSkillSets;
    string[,] _rightSkillSets;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        Transform[] children = gameObject.GetChildren();
        _leftInputHandlerArr = children[0].GetComponentsInChildren<SkillIconInputHandler>();
        _rightInputHandlerArr = children[2].GetComponentsInChildren<SkillIconInputHandler>();
        _leftSkillSets = new string[5, 9];
        _rightSkillSets = new string[5, 9];
        foreach (SkillIconInputHandler handler in _leftInputHandlerArr)
        {
            handler.Init();
        }
        foreach (SkillIconInputHandler handler in _rightInputHandlerArr)
        {
            handler.Init();
        }
    }
    public void UpdateSkill(bool isLeft, int index,string skillName)
    {
        if (isLeft) 
        {
            _leftSkillSets[_leftIndex, index] = skillName;
            return;
        }
        _rightSkillSets[_rightIndex, index] = skillName;

    }
    public void UpdateSkillIcon()
    {
        for(int i = 0; i < 9; i++)
        {
            _leftInputHandlerArr[i].UpdateSkill(_leftSkillSets[_leftIndex, i]);
            _rightInputHandlerArr[i].UpdateSkill(_rightSkillSets[_rightIndex, i]);
        }
    }
    public void SwitchLeft(int delta)
    {
        _leftIndex+=delta;
        if(_leftIndex > 4) { _leftIndex -= 5; }
        if(_leftIndex < 0) { _leftIndex += 5; }
        UpdateSkillIcon();
    }
    public void SwitchRight(int delta)
    {
        _rightIndex+=delta;
        if (_rightIndex > 4) { _rightIndex -= 5; }
        if (_rightIndex < 0) { _rightIndex += 5; }
        UpdateSkillIcon();
    }
    public void PutSkill(string newSkill)
    {
        for(int n = 0; n < 4; n++)
        {
            for(int m =0; m < 9; m++)
                {
                if (_leftSkillSets[n, m] == null) 
                { 
                    _leftSkillSets[n,m] = newSkill;
                    UpdateSkillIcon();
                    return;
                }
                if (_rightSkillSets[n, m] == null) 
                { 
                    _rightSkillSets[n, m] = newSkill;
                    UpdateSkillIcon();
                    return;
                }
            }
        }
    }
}
