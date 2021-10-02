using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public static class UnitLibrary
{
    static string _defualtPath = "UnitData/";
    static Dictionary<string, string> _unitJsonDict = new Dictionary<string, string>();
    public static void SetUnitData(string name,BaseUnitData unitData)
    {
        if (!_unitJsonDict.ContainsKey(name)) { _unitJsonDict.Add(name, Managers.DataMgr.GetJson(_defualtPath + $"{name}")); }
        Managers.DataMgr.PopulateObject(_unitJsonDict[name], unitData);
        var jo = JObject.Parse(_unitJsonDict[name])["SkillList"];
        List<string> list = JObject.Parse(_unitJsonDict[name])["SkillList"].ToObject<List<string>>();
        foreach (string skill in JObject.Parse(_unitJsonDict[name])["SkillList"].ToObject<List<string>>())
        {
            unitData.SkillDict.Add(skill, SkillLibrary.GetSkill(skill));
        }
    }
}
