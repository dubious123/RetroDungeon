using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager
{
    GameObject _dungeon;
    int RowSize, ColumnSize;
    ZTile _zTile = new ZTile();
    Vector3 TilePos = new Vector3();

    void Init()
    {
        
    }
    public GameObject CreateNewDungeon(Define.World world)
    {
        _dungeon = Managers.Resource.Instantiate("Dungeon");

        RowSize = (int)Random.Range((float)Define.DugeonSize.RowMin, (float)Define.DugeonSize.ColumnMax);
        ColumnSize = (int)Random.Range((float)Define.DugeonSize.ColumnMin, (float)Define.DugeonSize.ColumnMax);
        if (_dungeon == null)
        {
            Debug.Log($"Failed to create Dungeon");
            return null;
        }
        for(int i = 0; i < RowSize; i++)
        {
            for(int j = 0; j < ColumnSize; j++)
            {
                _zTile.CreateTile()
            }
        }
        SetDefaultTile(world);

        
        return _dungeon;
    }
    void SetDefaultTile(Define.World world)
    {

    }

}
