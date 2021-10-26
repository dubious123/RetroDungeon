using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator_ConwayLife : BaseDungeonGenerator
{
    int[,] _lifeMap;
    protected override void GenerateRest()
    {
        InitMap();
        for(int i = 0; i< _generationInfo.Iteration; i++)
        {
            GetNextGenMap();
        }
        ApplyResult();
    }
    void InitMap()
    {
        _lifeMap = new int[_width, _hight];
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _hight; y++)
            {
                _lifeMap[x, y] = Random.Range(0, 101) < _generationInfo.InitChance ? 1 : 0;
            }
        }
    }
    void GetNextGenMap()
    {
        int[,] nextMap = new int[_width, _hight];
        BoundsInt neighbors = new BoundsInt(-1, -1, 0, 3, 3, 1);
        Vector3Int nowPos;
        int liveCount;
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _hight; y++)
            {
                liveCount = 0;
                foreach (Vector3Int neighbor in neighbors.allPositionsWithin)
                {
                    if (neighbor == Vector3Int.zero) 
                    { 
                        continue; 
                    }
                    nowPos = neighbor + new Vector3Int(x, y, 0);
                    if (!_dungeon.IsInBound(nowPos)) 
                    {
                        liveCount += Random.Range(0,1);
                        continue;
                    }
                    if (_lifeMap[nowPos.x, nowPos.y] == 1)
                    {
                        liveCount++; 
                    }
                }
                if (_lifeMap[x, y] == 1 && liveCount < _generationInfo.DeathLimit)
                {
                    nextMap[x, y] = 0;
                    continue;
                }
                else if (_lifeMap[x, y] == 0 && liveCount > _generationInfo.BirthLimit)
                {
                    nextMap[x, y] = 1;
                    continue;
                }
                nextMap[x, y] = _lifeMap[x, y];
            }
        }
        _lifeMap = nextMap;
    }
    void ApplyResult()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _hight; y++)
            {
                if(_lifeMap[x,y] == 0) { _dungeon.GetTile(x, y).Type = _generationInfo.DeadTile; }
                else { _dungeon.GetTile(x, y).Type = _generationInfo.AliveTile; }
            }
        }
    }
}
