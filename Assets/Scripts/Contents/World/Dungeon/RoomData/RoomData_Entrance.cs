using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData_Entrance : BaseRoomData
{
    public RoomData_Entrance()
    {

    }
    protected override void Load()
    {
        Coor = new Vector2Int(2, 2);
        Type = Define.RoomType.Entrance;
        AliveSize = new Vector3Int(6,6,0);
        DeadSize = new Vector3Int(1,1,0);
        Range = 5;
    }
}
