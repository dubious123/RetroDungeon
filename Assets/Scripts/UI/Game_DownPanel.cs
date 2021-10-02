using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_DownPanel : MonoBehaviour
{
    int _leftIndex;
    int _rightIndex;
    SkillLibrary.BaseSkill[][] _leftSkillSets;
    SkillIconInputHandler[] _leftInputHandlerArr;
    SkillLibrary.BaseSkill[][] _rightSkillSets;
    SkillIconInputHandler[] _rightInputHandlerArr;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        Transform[] children = gameObject.GetChildren();
        _leftInputHandlerArr = children[0].GetComponentsInChildren<SkillIconInputHandler>();
        _rightInputHandlerArr = children[2].GetComponentsInChildren<SkillIconInputHandler>();
        foreach (SkillIconInputHandler handler in _leftInputHandlerArr)
        {
            handler.Init();
        }
        foreach (SkillIconInputHandler handler in _rightInputHandlerArr)
        {
            handler.Init();
        }
    }
    public void UpdateSkillIcon()
    {
        foreach(SkillIconInputHandler handler in _leftInputHandlerArr)
        {
            //Todo 
            if (Managers.GameMgr.Player_Data.Stat.Ap < 1) { handler.DisableSkill(); }
            else { handler.EnableSkill(); }
        }
        foreach (SkillIconInputHandler handler in _rightInputHandlerArr)
        {
            //Todo 
            if (Managers.GameMgr.Player_Data.Stat.Ap < 1) { handler.DisableSkill(); }
            else { handler.EnableSkill(); }
        }
    }
    public void SwitchLeft()
    {

    }
    public void SwitchRight()
    {

    }
}
