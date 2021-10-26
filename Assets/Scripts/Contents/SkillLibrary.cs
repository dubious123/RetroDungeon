using Newtonsoft.Json.Linq;
using System;

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Newtonsoft.Json;
using Random = UnityEngine.Random;

public static class SkillLibrary
{
    static List<string> _skillNameList;
    static string _defualtPath = "SkillData/";
    static Dictionary<string, string> _skillJsonDict = new Dictionary<string, string>();
    static SkillLibrary()
    {
        JObject jo = Managers.DataMgr.GetJObject(_defualtPath + "SkillList");
        _skillNameList = jo["SkillList"].ToObject<List<string>>();
    }
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
    public static BaseSkill GetRandomSkill()
    {
        return GetSkill(_skillNameList[Random.Range(0, _skillNameList.Count - 1)]);
    }
}
