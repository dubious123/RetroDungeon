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
        //������ ����
        
        if (item.Wearable)
        {
            //���� -> ����
            //������ -> �Ա�
        }
        if (item.Usable)
        {
            //���
        }
    }
    void Init_Skill(BaseSkill skill)
    {
        //��ų ����
        if(Managers.GameMgr.Player_Data.Stat.Ap >= skill.Cost)
        {
            //���
        }
    }
    void Content_Add()
    {

    }
}
