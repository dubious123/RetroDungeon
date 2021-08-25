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
        public int a;
        public abstract List<string> Tags { get; protected set; }
        public abstract int Cost { get; protected set; }
        public abstract int Range { get; protected set; }
    }
    public class Blunt : BaseSkill
    {
        public override List<string> Tags { get; protected set; } = new List<string>(
            new string[] { "Attack", "Melee" });
        public override int Cost { get; protected set; } = 1;
        public override int Range { get; protected set; } = 1;
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
