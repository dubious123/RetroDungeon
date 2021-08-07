using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public const int TileLayerNum = 7;
    public const int PlayerAnimatorNum = 4;
    public static int[] TileMoveCost =
    {
        1,1,1,1,3,3,3,3
    };
    public static Vector3[] TileIso4Dir =
    {
        _tileIsoDir.Left,
        _tileIsoDir.Up,
        _tileIsoDir.Right,
        _tileIsoDir.Down,
    };
    public static Vector3Int[] TileCoor4Dir =
{
        _tileCoorDir.YPlus,
        _tileCoorDir.XPlus,
        _tileCoorDir.XMinus,
        _tileCoorDir.YMinus,
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
    public static Vector3Int[] TileCoor8Dir =
    {
        _tileCoorDir.YPlus,
        _tileCoorDir.XPlus,
        _tileCoorDir.XMinus,
        _tileCoorDir.YMinus,
        _tileCoorDir.Up,
        _tileCoorDir.Right,
        _tileCoorDir.Down,
        _tileCoorDir.Left,
    };
    static class _tileIsoDir
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
    static class _tileCoorDir
    {
        //public static Vector3 LeftUp = new Vector3(-0.5f, 0.25f, 0);
        //public static Vector3 RightUp = new Vector3(0.5f, 0.25f, 0);
        //public static Vector3 RightDown = new Vector3(0.5f, -0.25f, 0);
        //public static Vector3 LeftDown = new Vector3(-0.5f, -0.25f, 0);
        public static Vector3Int YPlus = new Vector3Int(0, 1, 0);
        public static Vector3Int XPlus = new Vector3Int(1, 0, 0);
        public static Vector3Int XMinus = new Vector3Int(-1, 0, 0);
        public static Vector3Int YMinus = new Vector3Int(0, -1, 0);
        public static Vector3Int Up = new Vector3Int(1, 1, 0);
        public static Vector3Int Right = new Vector3Int(1, -1, 0);
        public static Vector3Int Down = new Vector3Int(-1, -1, 0);
        public static Vector3Int Left = new Vector3Int(-1, 1, 0);
    }
    public enum WeaponType
    {
        None,
        Sword,
        Rifle,
        Bow,
        Sycthe,
        Hammer,
    }
    public enum CharDir
    {
        Up,
        Right,
        Down,
        Left
    }
    public enum UnitState
    {
        Idle,
        Moving,
        Skill,
        UseItem,
        Die,
    }
    public enum Turn
    {
        Player,
        Enemy,
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
