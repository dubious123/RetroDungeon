using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager
{
    Dictionary<int, GameObject> _dungeons ;
    public int _currentLevel;
    public Dictionary<int,GameObject> Dungeons { get { return _dungeons; } }
    public int Level { get { return _currentLevel; } }
    public DungeonInfo CurrentDungeonInfo { get { return CurrentDungeon.GetComponent<DungeonInfo>(); } }
    public GameObject CurrentDungeon;

    DungeonGenerator _dungeonGenerator;
    public void Init()
    {
        _currentLevel = 0;
        _dungeons = new Dictionary<int, GameObject>();
    }
    public GameObject CreateNewDungeon(Define.World world)
    {
         _dungeonGenerator = new DungeonGenerator(world);
        CurrentDungeon = _dungeonGenerator.GenerateDungeon();
        _currentLevel++;
        Dungeons.Add(_currentLevel, CurrentDungeon);
        
        return CurrentDungeon;
    }
    public Dictionary<Vector3Int,TileInfo> GetTileInfoDict(int level = 0)
    {
        if(level == 0) { level = _currentLevel; }
        Managers.DungeonMgr.Dungeons.TryGetValue(level, out GameObject dungeon);
        return dungeon.GetComponent<DungeonInfo>().Board;
    }
    public void Clear()
    {
        _dungeons.Clear();
        _dungeons = null;
        _currentLevel = 0;
        _dungeonGenerator = null;
    }

}
