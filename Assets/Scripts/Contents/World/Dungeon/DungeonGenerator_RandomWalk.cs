using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
/// <summary>
/// Dungeon -> Tilemaps -> Tile
/// Dungeon -> DungeonInfo -> Dict(TilePos,TileInfo)
/// TileInfo -> Sprites 
/// </summary>
public class DungeonGenerator_RandomWalk : BaseDungeonGenerator
{
    PerlinNoiseHelper _noiseHelper;
    Dictionary<Vector3Int, float> _noiseDic;
    int[,] _walkMap;
    List<Vector3Int> _startPosList;
    protected override void GenerateRest()
    {
        InitMap();
        foreach (Vector3Int pos in _startPosList)
        {
            RandomWalk(pos);
        }
        ApplyResult();
    }
    void InitMap()
    {
        _walkMap = new int[_width, _hight];
        _startPosList = new List<Vector3Int>();
        for(int i = 0; i < _generationInfo.Iteration; i++)
        {
            _startPosList.Add(new Vector3Int(Random.Range(0,_width), Random.Range(0,_hight),0) );
        }
    }
    void RandomWalk(Vector3Int startPos)
    {
        Stack<Vector3Int> openStack = new Stack<Vector3Int>();
        openStack.Append(startPos);
        
        Vector3Int nowPos;
        Vector3Int nextPos;
        for (int i = 0; openStack.Count > 0 && i < _generationInfo.WalkCount ; i++) 
        {
            nowPos = openStack.Pop();
            CloseTile(nowPos);
            foreach (Vector3Int dir in Define.TileCoor8Dir)
            {
                nextPos = nowPos + dir;
                if (_dungeon.IsInBound(nextPos) && IsOpen(nextPos)&& !openStack.Contains(nextPos)) 
                { 
                    openStack.Push(nextPos);
                }
            }
        }
    }
    void CloseTile(Vector3Int pos)
    {
        _walkMap[pos.x, pos.y] = 1;
    }
    bool IsOpen(Vector3Int pos)
    {
        return _walkMap[pos.x, pos.y] == 0;
    }
    void ApplyResult()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _hight; y++)
            {
                if (_walkMap[x, y] == 0) { _dungeon.GetTile(x, y).Type = _generationInfo.DeadTile; }
                else { _dungeon.GetTile(x, y).Type = _generationInfo.AliveTile; }
            }
        }
    }
}
