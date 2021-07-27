using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public static class TileDir
    {
        public static Vector3 LeftUp = new Vector3(-0.5f, 0.25f, 0);
        public static Vector3 RightUp = new Vector3(0.5f, 0.25f, 0);
        public static Vector3 RightDown = new Vector3(0.5f, -0.25f, 0);
        public static Vector3 LeftDown = new Vector3(-0.5f, -0.25f, 0);
        public static Vector3 Left = new Vector3(+1.0f, 0, 0);
        public static Vector3 Up = new Vector3(0, 0.5f, 0);
        public static Vector3 Right = new Vector3(1.0f, 0, 0);
        public static Vector3 Down = new Vector3(0, -0.5f, 0);
    }
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
