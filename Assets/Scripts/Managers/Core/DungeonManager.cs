using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager
{
    BaseDungeonGenerator _generator;
    public void Init()
    {

    }
    Dungeon CreateNewDungeon_Conway(DungeonGenerationInfo info)
    {
        _generator = new DungeonGenerator_ConwayLife();
        return _generator.Generate(info);
    }
    void CreateNewDungeon_RandomWalk(DungeonGenerationInfo info)
    {

    }
    public Dungeon CreateNextDungeon(DungeonGenerationInfo info)
    {
        return CreateNewDungeon_Conway(info);
    }
    public void Clear()
    {
        _generator = null;
    }

}
