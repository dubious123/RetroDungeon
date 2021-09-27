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
        _dungeon = new Dungeon();
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
                _dungeon.GetTile(pos).Type = Define.TileType.Empty;
            }
            foreach (Vector3Int pos in new BoundsInt(startPoint - roomData.AliveSize/2,roomData.AliveSize).allPositionsWithin)
            {
                _dungeon.GetTile(pos).Type = Define.TileType.Default;
            }
            TileInfo tileInfo = _dungeon.GetTile(startPoint);
            tileInfo.Type = roomData.StartTileType;
            if(roomData.Type == Define.RoomType.Entrance)
            {
                tileInfo.PreWorld = roomData.PreWorld;
                _dungeon.EntrancePosList.Add(startPoint);
            }
            if(roomData.Type == Define.RoomType.Exit)
            {
                tileInfo.NextWorld = roomData.ExitWorld;
                _dungeon.ExitPosList.Add(startPoint);
            }
        }
    }
    Vector3Int GetRandomCoorInRange(BaseRoomData roomData)
    {
        return Vector3Int.RoundToInt(Random.insideUnitCircle * Random.Range(roomData.Range,0f) + roomData.Coor);
    }
    protected abstract void GenerateRest();
}
