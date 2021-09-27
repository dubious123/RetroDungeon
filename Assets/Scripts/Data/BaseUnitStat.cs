using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitStat
{

    public int Speed { get; set; } = 5;
    public float MoveSpeed { get; set; } = 6f;

    public int MaxAp { get; set; } = 4;
    public int RecoverAp { get; set; } = 3;
    public int Ap { get; set; } = 3;
    public int MaxHp { get; set; } = 50;
    public int Hp { get; set; } = 50;
    public int MaxDef { get; set; } = 10;
    public int Def { get; set; } = 10;
    public int MaxMs { get; set; } = 10;
    public int Ms { get; set; } = 10;
    public int MaxMp { get; set; } = 50;
    public int Mp { get; set; } = 50;
    public int MaxShock { get; set; } = 50;
    public int Shock { get; set; } = 0;
    public int MaxStress { get; set; } = 50;
    public int Stress { get; set; } = 0;

    public int EyeSight { get; set; } = 10;

}
