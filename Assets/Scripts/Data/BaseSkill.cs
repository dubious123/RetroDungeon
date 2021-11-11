using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : ISlot_Content
{
    public string Name { get; set; }   
    public string AnimName { get; set; } 
    public List<string> Tags { get; set; } = new List<string>();
    public int Cost { get; set; }
    public int Range { get; set; }
    public bool IsSelfIncluded { get; set; }
    public List<Vector2Int> Area { get; set; } = new List<Vector2Int>();
    public int AttackDamage { get; set; }
    public int MagicDamage { get; set; }
    public int MentalDamage { get; set; }
    public int ShockDamage { get; set; }
}
