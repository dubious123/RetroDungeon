using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public const int TileLayerNum = 7;
    public static Vector3[] TileIso4Dir =
    {
        _tileIsoDir.Left,
        _tileIsoDir.Up,
        _tileIsoDir.Right,
        _tileIsoDir.Down,
    };
    public static Vector3Int[] TileCart4Dir =
{
        _tileCartDir.Left,
        _tileCartDir.Up,
        _tileCartDir.Right,
        _tileCartDir.Down,
    };
    public static Vector3[] TileIso8Dir =
{
        _tileIsoDir.Left,
        _tileIsoDir.LeftUp,
        _tileIsoDir.Up,
        _tileIsoDir.RightUp,
        _tileIsoDir.RightDown,
        _tileIsoDir.Down,
        _tileIsoDir.LeftDown,
    };
    public static class _tileIsoDir
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
    public static class _tileCartDir
    {
        //public static Vector3 LeftUp = new Vector3(-0.5f, 0.25f, 0);
        //public static Vector3 RightUp = new Vector3(0.5f, 0.25f, 0);
        //public static Vector3 RightDown = new Vector3(0.5f, -0.25f, 0);
        //public static Vector3 LeftDown = new Vector3(-0.5f, -0.25f, 0);
        public static Vector3Int Left = new Vector3Int(-1, 0, 0);
        public static Vector3Int Up = new Vector3Int(0, 1, 0);
        public static Vector3Int Right = new Vector3Int(1, 0, 0);
        public static Vector3Int Down = new Vector3Int(0, -1, 0);
    }
    public enum TileType
    {
        Default,
        Entrance,
        Exit
    }
    public enum WorldObject
    {
        Player,
        Monster,
        Boss,
        
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
        Scattered,
        Centered,
        Cliff,
        num
    }
    public enum RoomSize
    {
        Default,
        Tiny,
        Small,
        Big,
        Huge,
        num
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
