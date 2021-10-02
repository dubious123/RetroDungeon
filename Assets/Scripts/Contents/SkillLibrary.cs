using Newtonsoft.Json.Linq;
using System;

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Newtonsoft.Json;

public static class SkillLibrary
{
    static string _defualtPath = "SkillData/";
    static Dictionary<string, string> _skillJsonDict = new Dictionary<string, string>();
    public static void ReplaceSkill(string name, BaseSkill skill)
    {
        if (!_skillJsonDict.ContainsKey(name)) { _skillJsonDict.Add(name, Managers.DataMgr.GetJson(_defualtPath + name)); }
        Managers.DataMgr.PopulateObject(_skillJsonDict[name], skill);
    }
    public static BaseSkill GetSkill(string name)
    {
        if (!_skillJsonDict.ContainsKey(name)) { _skillJsonDict.Add(name, Managers.DataMgr.GetJson(_defualtPath + name)); }
        return JsonConvert.DeserializeObject<BaseSkill>(_skillJsonDict[name]);
    }
}
