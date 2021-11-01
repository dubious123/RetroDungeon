using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemLibrary 
{
    static List<string> _itemNameList;
    static string _defualtPath = "ItemData/";
    static Dictionary<string, string> _itemlJsonDict = new Dictionary<string, string>();
    static ItemLibrary()
    {
        JObject jo = Managers.DataMgr.GetJObject(_defualtPath + "ItemList");
        _itemNameList = jo["ItemList"].ToObject<List<string>>();
    }
    public static void ReplaceItem(string name, BaseItem item)
    {
        if (!_itemlJsonDict.ContainsKey(name)) { _itemlJsonDict.Add(name, Managers.DataMgr.GetJson(CalculatePath(name))); }
        BaseItem bi = Managers.DataMgr.PopulateObject(_itemlJsonDict[name], item);
        if(bi.Usable) { bi.ItemUseContent = SkillLibrary.GetSkill(bi.SkillName); }
    }
    public static BaseItem GetItem(string name)
    {
        if (!_itemlJsonDict.ContainsKey(name)) { _itemlJsonDict.Add(name, Managers.DataMgr.GetJson(CalculatePath(name))); }
        BaseItem bi = Managers.DataMgr.PopulateObject(_itemlJsonDict[name], new BaseItem());
        if (bi.Usable) { bi.ItemUseContent = SkillLibrary.GetSkill(bi.SkillName); }
        return bi;
    }
    public static BaseItem GetRandomItem()
    {
        return GetItem(_itemNameList[Random.Range(0, _itemNameList.Count - 1)]);
    }
    private static string CalculatePath(string name)
    {
        int pos = name.IndexOf("_");
        return _defualtPath + name.Substring(0, pos) + "/" + name;  
    }
}
