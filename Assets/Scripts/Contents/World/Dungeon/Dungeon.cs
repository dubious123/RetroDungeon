using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dungeon 
{
    public WorldPosition ID;
    Dictionary<Vector3Int, TileInfo> _map;
    public int Width;
    public int Hight;
    public List<BaseUnitData> UnitList = new List<BaseUnitData>(); 
    public List<Vector3Int> EntrancePosList = new List<Vector3Int>();
    public List<Vector3Int> ExitPosList = new List<Vector3Int>();
    public void InitMap(int width,int hight)
    {
        _map = new Dictionary<Vector3Int, TileInfo>();
        for (int x = 0; x < width; x++) 
        {
            for(int y = 0; y < hight; y++)
            {
                _map.Add(new Vector3Int(x,y,0), new TileInfo(x, y));
            }
        }
        Width = width;
        Hight = hight;
    }
    public TileInfo GetTile(Vector3Int pos)
    {
        return GetTile(pos.x, pos.y);
    }
    public TileInfo GetTile(int x, int y)
    {
        if (IsInBound(x,y)) { return _map[new Vector3Int(x,y,0)]; }
        else { Debug.Log("position out of bound"); return null; }
    }
    public bool IsEmpty(int x, int y)
    {
        return GetTile(x, y)?.Type == Define.TileType.Empty;
    }
    public bool IsEmpty(Vector3Int pos)
    {
        return GetTile(pos).Type == Define.TileType.Empty;
    }
    public bool IsInBound(Vector3Int pos)
    {
        return _map.ContainsKey(pos);
    }
    public bool IsInBound(int x, int y)
    {
        return _map.ContainsKey(new Vector3Int(x,y,0));
    }
    public TileInfo GetRandomTile()
    {
        return GetTile(Random.Range(0, Width), Random.Range(0, Hight));
    }
    public List<Vector3Int> GetAllTiles()
    {
        List<Vector3Int> result = new List<Vector3Int>();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Hight; y++)
            {
                if (!IsEmpty(x, y)) { result.Add(new Vector3Int(x, y, 0)); }
            }
        }
        return result;
    }
    public List<Vector3Int> GetAllUnitPos()
    {
        List<Vector3Int> result = new List<Vector3Int>();
        foreach (BaseUnitData unit in UnitList)
        {
            result.Add(unit.CurrentCellCoor);
        }
        return result;
    }
}
