using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem
{
    public string ItemName;
    public bool Usable;   
    public bool Wearable;
    public string SkillName;
    public int MaxStack;
    public int CurrentStack;
    public BaseUnitStat EquipmentStat = new BaseUnitStat();
    public BaseSkill ItemUseContent;    

}
