using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_SlotContentInfo : MonoBehaviour
{
    [SerializeField] RectTransform _Self;
    [SerializeField] RectTransform _Content;
    [SerializeField] GameObject _TextBox_ItemInfo;
    [SerializeField] GameObject _TextBox_SkillInfo;
    [SerializeField] GameObject _TextBox_Wear;
    [SerializeField] GameObject _TextBox_TakeOff;
    [SerializeField] GameObject _TextBox_Use;
    private void Start()
    {   
        _Self.position = Managers.InputMgr.MouseScreenPos;
    }
    public void Init(ISlot_Content content)
    {
        if (content is BaseItem) Init_Item(content as BaseItem);
        else if (content is BaseSkill) Init_Skill(content as BaseSkill);
    }
    void Init_Item(BaseItem item)
    {
        //아이템 정보
        
        if (item.Wearable)
        {
            //착용 -> 벗기
            //미착용 -> 입기
        }
        if (item.Usable)
        {
            //사용
        }
    }
    void Init_Skill(BaseSkill skill)
    {
        //스킬 정보
        if(Managers.GameMgr.Player_Data.Stat.Ap >= skill.Cost)
        {
            //사용
        }
    }
    void Content_Add()
    {

    }
}
