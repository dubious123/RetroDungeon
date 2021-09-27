using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager
{
    public GameObject World_go;
    public Dictionary<WorldPosition, Dungeon> WorldMap;
    List<BaseWorldGenerationData> _worldGenerationInfoList;
    public void Init()
    {
        WorldMap = new Dictionary<WorldPosition, Dungeon>();
    }
    public void CreateWorld()
    {
        World_go = Managers.ResourceMgr.Instantiate("Dungeon");
        GenerateWorldGenerationInfoList();
        foreach (BaseWorldGenerationData worldData in _worldGenerationInfoList)
        {
            foreach (DungeonGenerationInfo info in worldData.DungeonInfoList)
            {
                WorldMap.Add(info.ID, Managers.DungeonMgr.CreateNextDungeon(info));
                SpawningPool.GenerateUnits(info, WorldMap[info.ID]);
            }
        }
    }
    void GenerateWorldGenerationInfoList()
    {
        _worldGenerationInfoList = new List<BaseWorldGenerationData>
        {
        new AbandonedMineShaft(),
        new CrystalLake()
        };
    }
    public void Clear()
    {

    }
}
