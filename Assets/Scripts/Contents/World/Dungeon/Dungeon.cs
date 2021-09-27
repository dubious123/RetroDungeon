using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dungeon 
{
    public WorldPosition ID;
    TileInfo[,] _map;
    public int Width;
    public int Hight;
    public List<KeyValuePair<Vector3Int, BaseUnitData>> UnitSpawnInfoList;
    public List<Vector3Int> EntrancePosList = new List<Vector3Int>();
    public List<Vector3Int> ExitPosList = new List<Vector3Int>();
    public void InitMap(int width,int hight)
    {
        _map = new TileInfo[width, hight];
        for (int x = 0; x < width; x++) 
        {
            for(int y = 0; y < hight; y++)
            {
                _map[x, y] = new TileInfo();
            }
        }
        Width = width;
        Hight = hight;
    }
    public void InitUnitSpawnInfoList()
    {
        UnitSpawnInfoList = new List<KeyValuePair<Vector3Int, BaseUnitData>>();
    }
    public TileInfo GetTile(Vector3Int pos)
    {
        return GetTile(pos.x, pos.y);
    }
    public TileInfo GetTile(int x, int y)
    {
        if (IsInBound(x,y)) { return _map[x, y]; }
        else { Debug.Log("position out of bound"); return null; }
    }
    public bool IsEmpty(int x, int y)
    {
        return GetTile(x, y).Type == Define.TileType.Empty;
    }
    public bool IsEmpty(Vector3Int pos)
    {
        return GetTile(pos).Type == Define.TileType.Empty;
    }
    public bool IsInBound(Vector3Int pos)
    {
        return IsInBound(pos.x,pos.y);
    }
    public bool IsInBound(int x, int y)
    {
        return !(x < 0 || x >= Width || y < 0 || y >= Hight);
    }
    public TileInfo GetRandomTile()
    {
        return _map[Random.Range(0, Width), Random.Range(0, Hight)];
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
}
