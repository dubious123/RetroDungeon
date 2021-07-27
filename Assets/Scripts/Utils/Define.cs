using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum DugeonSize
    {
        RowMax = 50,
        RowMin = 20,
        ColumnMax = 50,
        ColumnMin = 20,
    }
    public enum TilemapLayer
    {
        Floor,
        FloorOverlay,
        Pit,
        Wall,
        WallCollider
    }
    public enum TerrainType
    {
        Default,
        Cliff,
    }
    public enum World
    {
        Unknown,
        AbandonedMineShaft,
    }
    public enum MapType
    {
        Default,
        Boss,
        Shop,
    }
    public enum SceneType
    {
        Unknown,
        Login,
        Game,
    }


}
