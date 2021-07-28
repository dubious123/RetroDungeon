using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager
{
    GameObject _dungeon;
    DungeonGenerator _dungeonGenerator;
    public void Init()
    {

    }
    public GameObject CreateNewDungeon(Define.World world)
    {
         _dungeonGenerator = new DungeonGenerator(world);
        return _dungeonGenerator.GenerateDungeon();
    }


}
