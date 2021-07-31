using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager
{
    Dictionary<int, GameObject> _dungeons ;
    public int _level;
    public Dictionary<int,GameObject> Dungeons { get { return _dungeons; } }
    public int Level { get { return _level; } } 

    public GameObject CurrentDungeon;

    DungeonGenerator _dungeonGenerator;
    public void Init()
    {
        _level = 1;
        _dungeons = new Dictionary<int, GameObject>();
    }
    public GameObject CreateNewDungeon(Define.World world)
    {
         _dungeonGenerator = new DungeonGenerator(world);
        CurrentDungeon = _dungeonGenerator.GenerateDungeon();
        Dungeons.Add(_level, CurrentDungeon);
        _level++;
        return CurrentDungeon;
    }


}
