using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData_Exit : BaseRoomData
{
    public RoomData_Exit()
    {

    }
    protected override void Load()
    {
        Coor = new Vector2Int(250, 250);
        Type = Define.RoomType.Entrance;
        AliveSize = new Vector3Int(6,6,0);
        DeadSize = new Vector3Int(5,5,0);
        Range = 300;
    }
}
