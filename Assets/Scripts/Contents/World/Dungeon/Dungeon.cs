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
    public TileInfo GetOrSetTileInfo(Vector3Int pos)
    {
        return GetOrSetTileInfo(pos.x, pos.y);
    }
    public TileInfo GetOrSetTileInfo(int x, int y)
    {
        if (IsInBound(x,y)) { return _map[x, y]; }
        else { Debug.Log("position out of bound"); return null; }
    }
    public bool IsEmpty(Vector3Int pos)
    {
        return GetOrSetTileInfo(pos) == null;
    }
    public bool IsInBound(Vector3Int pos)
    {
        return IsInBound(pos.x,pos.y);
    }
    public bool IsInBound(int x, int y)
    {
        return !(x < 0 || x >= Width || y < 0 || y >= Hight);
    }

}
