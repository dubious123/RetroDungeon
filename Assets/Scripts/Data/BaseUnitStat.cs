using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitStat
{

    public int MaxAp { get; set; } 
    public int RecoverAp { get; set; } 
    public int Ap { get; set; } 
    public int MaxHp { get; set; } 
    public int Hp { get; set; } 
    public int MaxDef { get; set; } 
    public int Def { get; set; } 
    public int MaxMs { get; set; } 
    public int Ms { get; set; }
    public int MaxMp { get; set; } 
    public int Mp { get; set; } 
    public int MaxShock { get; set; } 
    public int Shock { get; set; } 
    public int MaxStress { get; set; } 
    public int Stress { get; set; } 

    public int EyeSight { get; set; } 

    public int Priority { get; set; } 
    public float MoveSpeed { get; set; } 



    public int AttackDamage { get; set; }
    public int MagicDamage { get; set; }
    public int MentalDamage { get; set; }
    public int ShockDamage { get; set; }
    public int AttackDamage_Percentage { get; set; }
    public int MagicDamage_Percentage { get; set; }
    public int MentalDamage_Percentage { get; set; }
    public int ShockDamage_Percentage { get; set; }
}
