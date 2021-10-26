using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseWorldGenerationData 
{
    public DungeonGenerationInfo[] DungeonInfoList;
    public DungeonGenerationInfo FisrtDungeonInfo { get { return DungeonInfoList[0]; } }
    public DungeonGenerationInfo LastDungeonInfo { get { return DungeonInfoList[DungeonCount-1]; } }
    [SerializeField] protected int _count;
    protected Define.World _world;
    public int DungeonCount { get { return _count; } }
    protected BaseWorldGenerationData()
    {
    }
    public void LoadData(string path)
    {
        JObject jo = Managers.DataMgr.GetJObject(path);
        _count = jo["_count"].Value<int>();
        DungeonInfoList = new DungeonGenerationInfo[_count];
        DungeonInfoList = jo["DungeonInfoList"].ToObject<DungeonGenerationInfo[]>();
        //List<BaseRoomData> roomList = new List<BaseRoomData>();
        //roomList = ;
        for (int i = 0; i < _count; i++)
        {
            foreach (JObject room_jo in jo["DungeonInfoList"][i]["RoomSet"].ToObject<List<JObject>>())
            {
                BaseRoomData room;
                switch (room_jo["Type"].ToObject<Define.RoomType>())
                {
                    case Define.RoomType.Entrance:
                        room = new RoomData_Entrance();
                        Managers.DataMgr.PopulateObject(room_jo, room);
                        break;
                    case Define.RoomType.Exit:
                        room = new RoomData_Exit();
                        Managers.DataMgr.PopulateObject(room_jo, room);
                        break;
                    case Define.RoomType.HiddenIsland:
                        room = new RoomData_HiddenIsland();
                        Managers.DataMgr.PopulateObject(room_jo, room);
                        break;
                    default:
                        room = new BaseRoomData();
                        break;
                }
                DungeonInfoList[i].StaticRoomList.Add(room);
            }
        }

    }
}
