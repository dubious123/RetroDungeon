using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    string _dafaultPath = "Data/";
    public void Init()
    {

    }
    public void Clear()
    {

    }
    public T PopulateObject<T>(JObject jo, T obj)
    {
        JsonConvert.PopulateObject(jo.ToString(), obj);
        return obj;
    }
    public T PopulateObject<T>(string str,T obj)
    {
        if (str.Contains("/"))
        {
            JsonConvert.PopulateObject(GetJson(str), obj);
            return obj;
        }
        JsonConvert.PopulateObject(str, obj);
        return obj;

    }
    public JObject GetJObject(string path)
    {
        return JObject.Parse(GetJson(path));
    }
    public string GetJson(string path)
    {
        return Managers.ResourceMgr.Load<TextAsset>($"{_dafaultPath}{path}").text;
    }
    public void GetRoomData(string name)
    {
    }
}
