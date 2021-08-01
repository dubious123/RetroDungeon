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
    Vector3Int _gridPosition = Vector3Int.zero;
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
    void RandomWalk(Vector3Int startCartPosition, int walkLength)
    {
        _gridPosition = startCartPosition;
        for (int i = 0; i < walkLength; i++)
        {
            _gridPosition += Define.TileCart4Dir[Random.Range(0, Define.TileIso4Dir.Length)];
            
            if (_dungeonInfo.Board.ContainsKey(_gridPosition))
            {
                i--;
                continue;
            }
            _tile = new TileInfo(_world);
            _dungeonInfo.Board.Add(_gridPosition, _tile);
            _tilemaps.SetTile(_gridPosition, _tile);
            
        }
    }
    void ProcedureGenerator()
    {
        #region Set StartPosition (0,0,0)
        _gridPosition = new Vector3Int(0, 0, 0);
        _tile = new TileInfo(_world, Define.TileType.Entrance);
        _dungeonInfo.Board.Add(_gridPosition, _tile);
        _tilemaps.SetTile(_gridPosition, _tile);
        #endregion
        for (int i = 0; i < _dungeonInfo.Iteration; i++)
        {
            RandomWalk(_gridPosition, _dungeonInfo.RoomSize);
            _gridPosition = _dungeonInfo.Board.ElementAt(Random.Range(0, _dungeonInfo.Board.Count - 1)).Key;
        }
        #region Set Exit
        while (_gridPosition == Vector3Int.zero)
        {
            _gridPosition = _dungeonInfo.Board.ElementAt(Random.Range(0, _dungeonInfo.Board.Count - 1)).Key;
        }
        _tile = _dungeonInfo.Board[_gridPosition];
        _tile.tileType = Define.TileType.Exit;
        _tilemaps.SetTile(_gridPosition, _tile);
        #endregion
    }
}
