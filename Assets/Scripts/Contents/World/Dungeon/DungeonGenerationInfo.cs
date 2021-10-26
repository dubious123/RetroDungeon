using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerationInfo
{
    public DungeonGenerationInfo NextDungeonInfo;

    public WorldPosition ID;

    public int Width = 500;
    public int Hight = 500;
    public int Iteration = 4;
    public int DeathLimit = 2;
    public int BirthLimit = 3;
    public int InitChance = 70;
    public int WalkCount = 400;
    //public int RandomBirthChance;
    public Define.TileType AliveTile = Define.TileType.Default;
    public Define.TileType DeadTile = Define.TileType.Empty;
    public List<BaseRoomData> StaticRoomList = new List<BaseRoomData>();
    public Dictionary<string, int> UnitList = new Dictionary<string, int>();
}
