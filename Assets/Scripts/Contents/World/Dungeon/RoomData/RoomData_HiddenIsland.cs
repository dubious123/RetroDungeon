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
        AliveRange = 5;
        DeadRange = 20;
        Range = 4;
    }
}
