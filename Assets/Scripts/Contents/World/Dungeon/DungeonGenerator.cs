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
    List<Vector3Int> _endPosList;
    List<Vector3Int> _startPosList;
    float _maxAltitude = 0.8f;
    float _minAltitude = 0.4f;
    int _maxLakeSize = 10;
    int _maxRiverLength = 100;
    int _minRiverlength = 20;
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

        _tilemaps = _dungeonInfo.Tilemaps;
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
                    Vector3Int newPos = pos + new Vector3Int(x, y, 0);
                    if (_dungeonInfo.Board.ContainsKey(newPos) && _noiseDic[newPos] < _noiseDic[pos] + 0.1f) 
                    {
                        _dungeonInfo.Board[pos + new Vector3Int(x, y, 0)].Type = Define.TileType.Water;
                    }
                }
            }
        }
    }
    private IEnumerable<Vector3Int> CalculateLakeStartPos()
    {
        _endPosList = new List<Vector3Int>();
        int count = 0;
        foreach(KeyValuePair<Vector3Int,float> pair in _noiseDic)
        {
            if(pair.Value > _minAltitude) { continue; }
            if(CheckNeighbours(pair, neighbourNoise => neighbourNoise < pair.Value)) 
            {
                _endPosList.Add(pair.Key);
                count++;
            }
        }
        if(count < _dungeonInfo.LakeCount) { _dungeonInfo.LakeCount = count; }
        return _endPosList.OrderBy(pos => _noiseDic[pos]).Take(Random.Range(0,_dungeonInfo.LakeCount));
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
        foreach (Vector3Int pos in CalculateRiverStartPos())
        {
            if(Random.Range(0, 2) == 0) { CreateRiver(pos,_endPosList.OrderBy(endPos=>(pos-endPos).magnitude).First()); }
            else { CreateRiver(pos); }             
        }
    }

    private void CreateRiver(Vector3Int start)
    {
        int riverLength = Random.Range(_minRiverlength, _maxRiverLength);
        for(int i = 0; i < riverLength; i++)
        {

        }
    }

    private void CreateRiver(Vector3Int start, Vector3Int end)
    {
        Vector3Int prePos = start;
        Vector3Int nowPos = start;
        for(;nowPos != end;nowPos = GetNextDirToEndPos())
        {

        }
    }
    private Vector3Int GetNextDirToEndPos()
    {
        Vector3Int dir = Vector3Int.zero;

        return dir;
    }
    private IEnumerable<Vector3Int> CalculateRiverStartPos()
    {
        _startPosList = new List<Vector3Int>();
        int count = 0;
        foreach (KeyValuePair<Vector3Int, float> pair in _noiseDic)
        {
            if (pair.Value > _minAltitude) { continue; }
            if (CheckNeighbours(pair, neighbourNoise => neighbourNoise > pair.Value))
            {
                _startPosList.Add(pair.Key);
                count++;
            }
        }
        if (count < _dungeonInfo.RiverCount) { _dungeonInfo.RiverCount = count; }
        return _startPosList.OrderByDescending(pos => _noiseDic[pos]).Take(Random.Range(0,_dungeonInfo.RiverCount));
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
