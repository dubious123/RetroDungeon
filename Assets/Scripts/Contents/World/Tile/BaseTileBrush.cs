using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[Serializable]
public class BaseTileBrush 
{
    protected Tilemap[] _tilemaps;
    [SerializeField]
    protected string _defaultPrefabPath;
    [SerializeField]
    protected Dictionary<string, TileBase[]> _tilesDict;
    int _length;
    public BaseTileBrush()
    {
        _tilemaps = Managers.WorldMgr.World_go.GetComponentsInChildren<Tilemap>();
        _length = _tilemaps.Length;
        _tilesDict = new Dictionary<string, TileBase[]>()
        {
            {"Empty",new TileBase[_length] },
            {"Default",new TileBase[_length] },
            {"Water",new TileBase[_length] },
            {"Lava",new TileBase[_length] },
            {"Entrance",new TileBase[_length] },
            {"Exit",new TileBase[_length] }
        };
    }
    public virtual void Paint(Vector3Int pos, TileInfo tile)
    {
        _tilemaps.SetTile(pos, _tilesDict[$"{tile.Type}"]);
    }
    protected void LoadData(string path)
    {
        JObject jo = Managers.DataMgr.GetJObject(path);
        _defaultPrefabPath = jo["_defaultPrefabPath"].Value<string>();
        foreach(var pairB in jo["_tilesDict"].ToObject<Dictionary<string, Dictionary<int,string>>>())
        {
            foreach(var pairS in pairB.Value)
            {
                _tilesDict[pairB.Key][pairS.Key] = Managers.ResourceMgr.Load<TileBase>($"{_defaultPrefabPath}{pairS.Value}");
            }
        }
    }
}
