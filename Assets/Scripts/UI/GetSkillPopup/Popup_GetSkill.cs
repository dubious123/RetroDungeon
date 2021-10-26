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
    [SerializeField] RectTransform _SkillPanel;
    private void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_SkillPanel);
        GetRandomSkill();
        UpdateUI();
    }
    void GetRandomSkill()
    {
        for(int i = 0; i < 3; i++)
        {
            _randomSkillArr[i] = SkillLibrary.GetRandomSkill();
        }
    }
    void UpdateUI()
    {
        for(int i = 0; i < 3; i++)
        {
            _SkillTexts[i].text = _randomSkillArr[i].Name;
            _SkillImages[i].sprite = Managers.ResourceMgr.GetSkillSprite(_randomSkillArr[i].Name);
        }
    }
    public void GetSkill(int num)
    {
        Managers.GameMgr.Player_Data.LearnSkill(_randomSkillArr[num]);
        Managers.ResourceMgr.Destroy(gameObject);
    }
}
