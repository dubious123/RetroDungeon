using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager
{
    int RowSize, ColumnSize;
    GameObject _dungeon;
    Vector3 TilePos = new Vector3();

    void Init()
    {
        
    }
    public GameObject CreateNewDungeon(Define.World world)
    {
        _dungeon = Managers.Resource.Instantiate("Dungeon");
        if (_dungeon == null)
        {
            Debug.Log($"Failed to create Dungeon");
            return null;
        }
        DungeonInfo _dungeonInfo = _dungeon.GetOrAddComponent<DungeonInfo>();
        RowSize = (int)Random.Range((float)Define.DugeonSize.RowMin, (float)Define.DugeonSize.ColumnMax);
        ColumnSize = (int)Random.Range((float)Define.DugeonSize.ColumnMin, (float)Define.DugeonSize.ColumnMax);
        _dungeonInfo.Init(RowSize, ColumnSize, world, _dungeon);



        
        return _dungeon;
    }


}
