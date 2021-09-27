using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData_HiddenIsland : BaseRoomData
{
    public RoomData_HiddenIsland()
    {

    }
    protected override void Load()
    {
        Coor = new Vector2Int(400, 300);
        Type = Define.RoomType.Entrance;
        AliveSize = new Vector3Int(5,5,0);
        DeadSize = new Vector3Int(10,10,0);
        Range = 4;
    }
}
