using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLibrary
{
    static SkillLibrary _instance;
    static void Init()
    {
        if(_instance == null)
        {
            _instance = new SkillLibrary();
        }
    }
    public static BaseSkill GetSkill(string name)
    {
        return (BaseSkill)_instance.GetType().GetField(name).GetValue(_instance);
    }
    public abstract class BaseSkill
    {
        public abstract List<string> Tags { get; protected set; }
        public abstract int Cost { get; protected set; }
        public abstract int Range { get; protected set; }
    }
    public class Blunt : BaseSkill
    {
        public override List<string> Tags { get; protected set; } = new List<string>(
            new string[] { "Attack","Melee" });
        public override int Cost { get; protected set; } = 1;
        public override int Range { get; protected set; } = 1;
    }

}
