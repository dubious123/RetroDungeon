using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoomData
{
    public Vector2Int Coor;
    public int Range;
    public Define.RoomType Type;
    public Define.TileType StartTileType;
    public Vector3Int AliveSize;
    public Vector3Int DeadSize;
    public WorldPosition PreWorld;
    public WorldPosition ExitWorld;
    public BaseRoomData()
    {
        Load();

    }
    protected abstract void Load();
}
