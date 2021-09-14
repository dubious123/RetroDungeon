using System;

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class SkillLibrary
{
    static Library _instance;
    static SkillLibrary()
    {
        _instance = new Library();
    }
    public static BaseSkill GetSkill(string name)
    {
        Type type = typeof(Library);
        FieldInfo field = type.GetField("Skill_" + name);
        object obj = field.GetValue(_instance);
        return (BaseSkill)obj;
    }

    public abstract class BaseSkill
    {
        public abstract string Name { get; protected set; }
        public abstract string AnimName { get; protected set; }
        public abstract List<string> Tags { get; protected set; }
        public abstract int Cost { get; protected set; }
        public abstract int Range { get; protected set; }
        public abstract List<Vector2Int> Area { get; protected set; }
        public abstract int PhysicalDamage { get; protected set; }
        public abstract int MagicDamage { get; protected set; }
        public abstract int MentalDamage { get; protected set; }
        public abstract int ShockDamage { get; protected set; }
    }
    public class Blunt : BaseSkill
    {
        public override string Name { get; protected set; } = "Blunt";
        public override string AnimName { get; protected set; } = "throw";
        public override List<string> Tags { get; protected set; } = new List<string>(
            new string[] { "Attack", "Melee" , "SingleTarget" });
        public override int Cost { get; protected set; } = 1;
        public override int Range { get; protected set; } = 1;
        public override List<Vector2Int> Area { get; protected set; } = new List<Vector2Int>
        {
            Vector2Int.zero
        };
        public override int PhysicalDamage { get; protected set; } = 50;
        public override int MagicDamage { get; protected set; } = 0;
        public override int MentalDamage { get; protected set; } = 0;
        public override int ShockDamage { get; protected set; } = 5;
    }

    class Library
    {
        public static Blunt Skill_Blunt;
        public Library()
        {
            Skill_Blunt = new Blunt();
        }
    }
}
