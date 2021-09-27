using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDungeonGenerator
{
    protected DungeonGenerationInfo _generationInfo;
    protected Dungeon _dungeon;
    protected int _width;
    protected int _hight;
    public Dungeon Generate(DungeonGenerationInfo dungeonGenerationInfo)
    {
        _generationInfo = dungeonGenerationInfo;
        _dungeon.ID = _generationInfo.ID;
        _width = dungeonGenerationInfo.Width;
        _hight = dungeonGenerationInfo.Hight;
        _dungeon.InitMap(_width, _hight);
        GenerateRest();
        GenerateStaticRoom();
        return _dungeon;
    }
    void GenerateStaticRoom()
    {
        foreach (BaseRoomData roomData in _generationInfo.StaticRoomList)
        {   
            Vector3Int startPoint = GetRandomCoorInRange(roomData);
            foreach (Vector3Int pos in new BoundsInt(startPoint - roomData.DeadSize / 2, roomData.DeadSize).allPositionsWithin)
            {
                _dungeon.GetOrSetTileInfo(pos).Type = Define.TileType.Empty;
            }
            foreach (Vector3Int pos in new BoundsInt(startPoint - roomData.AliveSize/2,roomData.AliveSize).allPositionsWithin)
            {
                _dungeon.GetOrSetTileInfo(pos).Type = Define.TileType.Default;
            }
            TileInfo tileInfo = _dungeon.GetOrSetTileInfo(startPoint);
            tileInfo.Type = roomData.StartTileType;
            if(roomData.Type == Define.RoomType.Entrance)
            {
                tileInfo.PreWorld = roomData.PreWorld;
            }
            if(roomData.Type == Define.RoomType.Exit)
            {
                tileInfo.NextWorld = roomData.ExitWorld;
            }
        }
    }
    Vector3Int GetRandomCoorInRange(BaseRoomData roomData)
    {
        return Vector3Int.RoundToInt(Random.insideUnitCircle * roomData.Range + roomData.Coor);
    }
    protected abstract void GenerateRest();
}
