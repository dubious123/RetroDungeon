using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWorldGenerationData
{
    public DungeonGenerationInfo[] DungeonInfoList;
    public DungeonGenerationInfo FisrtDungeonInfo { get { return DungeonInfoList[0]; } }
    public DungeonGenerationInfo LastDungeonInfo { get { return DungeonInfoList[DungeonCount-1]; } }
    protected int _count;
    protected Define.World _world;
    public int DungeonCount { get { return _count; } }
    protected BaseWorldGenerationData()
    {
        LoadData();
        DungeonInfoList = new DungeonGenerationInfo[DungeonCount];
        for (int i = 0; i < DungeonCount; i++)
        {
            DungeonGenerationInfo info = new DungeonGenerationInfo();
            DungeonInfoList[i] = info;
            info.ID = new WorldPosition(_world,i+1);      
        }
    }
    public abstract void LoadData();
}
