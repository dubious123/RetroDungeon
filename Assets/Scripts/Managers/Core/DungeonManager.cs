using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager
{
    GameObject Dungeon;
    DungeonGenerator _dungeonGenerator;
    public void Init()
    {

    }
    public GameObject CreateNewDungeon(Define.World world)
    {
         _dungeonGenerator = new DungeonGenerator(world);
        Dungeon = _dungeonGenerator.GenerateDungeon();
        return Dungeon;
    }


}
