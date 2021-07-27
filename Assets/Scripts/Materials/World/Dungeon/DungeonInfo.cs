using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonInfo : MonoBehaviour
{
    int _rowSize, _columnSize;
    [SerializeField]
    public ZTile[,] Board;
    public ZTile _baseTileInfo;
    GameObject _dungeon;
    public GameObject _baseTile;
    public void Init(int RowSize,int ColumnSize, Define.World world, GameObject Dungeon)
    {
        _dungeon = Dungeon;
        _rowSize = RowSize;
        _columnSize = ColumnSize;
        Board = new ZTile[_rowSize, ColumnSize];
        SetDefaultTile(world);
        for (int i = 0; i < _rowSize; i++)
        {
            for (int j = 0; j < _columnSize; j++)
            {
                _baseTile = Managers.Resource.Instantiate("Tiles/BaseTile",_dungeon.transform);
                Board[i, j] = _baseTile.GetOrAddComponent<ZTile>();
                Board[i, j].SetPos(new Vector2(i,j));
            }
        }
    }
    void SetDefaultTile(Define.World world)
    {
        _baseTileInfo = new ZTile(world);
    }
}
