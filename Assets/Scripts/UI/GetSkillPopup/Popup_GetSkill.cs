using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup_GetSkill : MonoBehaviour
{
    BaseSkill[] _randomSkillArr = new BaseSkill[3];
    [SerializeField] TextMeshProUGUI[] _SkillTexts;
    [SerializeField] Image[] _SkillImages;
    private void Awake()
    {
        GetRandomSkill();
        UpdateUI();
    }
    void GetRandomSkill()
    {
        for(int i = 0; i < 3; i++)
        {
            _randomSkillArr[i] = SkillLibrary.GetSkill("Blunt");
        }
    }
    void UpdateUI()
    {
        for(int i = 0; i < 3; i++)
        {
            _SkillTexts[i].text = _randomSkillArr[i].Name;
            _SkillImages[i].sprite = Managers.ResourceMgr.GetSprite(_randomSkillArr[i].Name);
        }
    }
}
