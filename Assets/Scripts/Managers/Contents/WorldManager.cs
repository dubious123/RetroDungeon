using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager
{
    public GameObject World_go;
    public Dictionary<WorldPosition, Dungeon> WorldMap;
    List<BaseWorldGenerationData> _worldGenerationInfoList;
    SpawningPool _pool;
    public void Init()
    {
        WorldMap = new Dictionary<WorldPosition, Dungeon>();
    }
    public void CreateWorld()
    {
        if(_pool == null) { _pool = GameObject.Find("Units").GetComponent<SpawningPool>(); }
        World_go = Managers.ResourceMgr.Instantiate("Dungeon");
        GenerateWorldGenerationInfoList();
        foreach (BaseWorldGenerationData worldData in _worldGenerationInfoList)
        {
            foreach (DungeonGenerationInfo info in worldData.DungeonInfoList)
            {
                WorldMap.Add(info.ID, Managers.DungeonMgr.CreateNextDungeon(info));
                _pool.GenerateUnits(info, WorldMap[info.ID]);
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
