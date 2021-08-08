using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// Dungeon -> Tilemaps -> Tile
/// Dungeon -> DungeonInfo -> Dict(TilePos,TileInfo)
/// TileInfo -> Sprites 
/// </summary>
public class DungeonGenerator
{
    Define.World _world;
    GameObject _dungeon;
    DungeonInfo _dungeonInfo;
    Tilemap[] _tilemaps;
    Vector3Int _cellPosition = Vector3Int.zero;
    TileInfo _tile;
    public DungeonGenerator(Define.World world)
    {
        Init(world);
    }
    void Init(Define.World world)
    {
        _world = world;
        _dungeon = Managers.ResourceMgr.Instantiate("Dungeon");
        if (_dungeon == null)
        {
            Debug.Log($"Failed to create Dungeon");
            return ;
        }
        _dungeonInfo = _dungeon.GetOrAddComponent<DungeonInfo>();
        _dungeonInfo.Init(_world);

        _tilemaps = _dungeonInfo.tilemaps;

    }



    public GameObject GenerateDungeon()
    {
        ProcedureGenerator();
        return _dungeon;
    }
    void RandomWalk(Vector3Int startCellPosition, int walkLength)
    {
        _cellPosition = startCellPosition;
        for (int i = 0; i < walkLength; i++)
        {
            _cellPosition += Define.TileCoor4Dir[Random.Range(0, Define.TileIso4Dir.Length)];
            
            if (_dungeonInfo.Board.ContainsKey(_cellPosition))
            {
                i--;
                continue;
            }
            _tile = new TileInfo(_world);
            _dungeonInfo.Board.Add(_cellPosition, _tile);
            _tilemaps.SetTile(_cellPosition, _tile);
            
        }
    }
    void ProcedureGenerator()
    {
        #region Set StartPosition (0,0,0)
        _cellPosition = new Vector3Int(0, 0, 0);
        _tile = new TileInfo(_world, Define.TileType.Entrance);
        _dungeonInfo.Board.Add(_cellPosition, _tile);
        _tilemaps.SetTile(_cellPosition, _tile);
        #endregion
        for (int i = 0; i < _dungeonInfo.Iteration; i++)
        {
            RandomWalk(_cellPosition, _dungeonInfo.RoomSize);
            _cellPosition = _dungeonInfo.Board.ElementAt(Random.Range(0, _dungeonInfo.Board.Count - 1)).Key;
        }
        #region Set Exit
        while (_cellPosition == Vector3Int.zero)
        {
            _cellPosition = _dungeonInfo.Board.ElementAt(Random.Range(0, _dungeonInfo.Board.Count - 1)).Key;
        }
        _tile = _dungeonInfo.Board[_cellPosition];
        _tile.tileType = Define.TileType.Exit;
        _tilemaps.SetTile(_cellPosition, _tile);
        #endregion
    }
}
