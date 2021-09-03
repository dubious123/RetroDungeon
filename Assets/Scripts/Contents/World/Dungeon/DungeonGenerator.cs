using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
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
    PerlinNoiseHelper _noiseHelper;
    Dictionary<Vector3Int, float> _noiseDic;
    float _maxAltitude = 0.8f;
    float _minAltitude = 0.2f;
    int _maxLakeSize = 5;
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
        _noiseHelper = new PerlinNoiseHelper();
        _noiseDic = new Dictionary<Vector3Int, float>();
    }
    public GameObject GenerateDungeon()
    {
        ProcedureGenerator();
        GenerateLake();
        GenerateRiver();
        GenerateEntranceAndExit();
        GenerateOverlay();
        
        RenderDungeon();
        return _dungeon;
    }
    void ProcedureGenerator()
    {
        #region Set StartPosition (0,0,0)
        _cellPosition = new Vector3Int(0, 0, 0);
        _tile = new TileInfo(_world, Define.TileType.Entrance);
        _noiseDic.Add(_cellPosition, _noiseHelper.GetNoise(_cellPosition));
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
        _tile.Type = Define.TileType.Exit;
        _tilemaps.SetTile(_cellPosition, _tile);
        #endregion
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
            _noiseDic.Add(_cellPosition, _noiseHelper.GetNoise(_cellPosition));
            _dungeonInfo.Board.Add(_cellPosition, _tile);
            _tilemaps.SetTile(_cellPosition, _tile);
            
        }
    }
    private void GenerateLake()
    {
        int lakeSize;
        foreach(Vector3Int pos in CalculateLakeStartPos())
        {
            lakeSize = Random.Range(0, _maxLakeSize);
            for (int x = -lakeSize ; x < lakeSize; x++)
            {
                for(int y = -lakeSize; y < lakeSize; y++)
                {
                    if(_dungeonInfo.Board.ContainsKey(pos + new Vector3Int(x, y, 0)))
                    {
                        _dungeonInfo.Board[pos + new Vector3Int(x, y, 0)].Type = Define.TileType.Water;
                    }
                }
            }
        }
    }
    private List<Vector3Int> CalculateLakeStartPos()
    {
        List<Vector3Int> posList = new List<Vector3Int>();
        int count = 0;
        foreach(KeyValuePair<Vector3Int,float> pair in _noiseDic)
        {
            if(pair.Value > _minAltitude) { continue; }
            if(CheckNeighbours(pair, neighbourNoise => neighbourNoise < pair.Value)) 
            { 
                posList.Add(pair.Key);
                count++;
                if(count >= _dungeonInfo.LakeCount) { break; }
            }
        }
        if(count < _dungeonInfo.LakeCount) { _dungeonInfo.LakeCount = count; }
        return posList;
    }

    private bool CheckNeighbours(KeyValuePair<Vector3Int, float> pair, Func<float, bool> failCondition)
    {
        foreach(Vector3Int dir in Define.TileCoor4Dir)
        {
            if(!_dungeonInfo.Board.ContainsKey(pair.Key + dir) || failCondition(_noiseDic[pair.Key + dir])) 
            {
                return false;
            }
        }
        return true;
    }

    private void GenerateRiver()
    {

    }
    private void GenerateEntranceAndExit()
    {

    }
    private void GenerateOverlay()
    {

    }
    private void RenderDungeon()
    {
        foreach(KeyValuePair<Vector3Int,TileInfo> pair in _dungeonInfo.Board)
        {
            pair.Value.SetTileDetails();
            _tilemaps.SetTile(pair.Key, pair.Value);
        }
    }
}
