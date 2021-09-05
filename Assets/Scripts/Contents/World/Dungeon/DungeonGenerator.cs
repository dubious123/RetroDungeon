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
    int _maxLakeSize = 20;
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
        _cellPosition = new Vector3Int(0, 0, 0);
        for (int i = 0; i < _dungeonInfo.Iteration; i++)
        {
            RandomWalk(_cellPosition, _dungeonInfo.RoomSize);
            _cellPosition = _dungeonInfo.Board.ElementAt(Random.Range(0, _dungeonInfo.Board.Count - 1)).Key;
        }
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
            _dungeonInfo.lake.Add(pos);
            lakeSize = Random.Range(0, _maxLakeSize);
            for (int x = -lakeSize ; x < lakeSize; x++)
            {
                for(int y = -lakeSize; y < lakeSize; y++)
                {
                    Vector3Int newPos = pos + new Vector3Int(x, y, 0);
                    if (_noiseHelper.GetNoise(newPos) > _noiseDic[pos] + 0.2f) { continue; }
                    if (_dungeonInfo.Board.ContainsKey(newPos)) { _dungeonInfo.Board[newPos].Type = Define.TileType.Water; }
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
        _dungeonInfo.riverstart.Add(start);
        List<Vector3Int> river = new List<Vector3Int>();
        int riverLength = Random.Range(_minRiverlength, _maxRiverLength);
        Vector3 nowPos = start;
        Vector3Int nowCellPos = start;
        Vector3 nowDir = Define.TileCoor8Dir.OrderBy(dir =>
            _noiseDic.TryGetValue(start + dir, out float noise) ? noise : float.MaxValue).First();
        for (int i = 0; i < riverLength; i++)
        {
            nowDir = Define.TileCoor8Dir.OrderBy(
                dir => Vector3.Dot(dir, nowDir) >= 0 && !river.Contains(Vector3Int.RoundToInt(nowPos + dir)) ?
                _noiseHelper.GetNoise(nowPos + dir) : 1)
                .First();
            nowDir = nowDir.normalized;
            nowPos += nowDir;
            nowCellPos = Vector3Int.RoundToInt(nowPos);
            if (_dungeonInfo.Board.TryGetValue(nowCellPos, out TileInfo tile)) { tile.Type = Define.TileType.Water; }
            else { _dungeonInfo.Board.Add(nowCellPos, new TileInfo(_world, Define.TileType.Water)); }
            river.Add(nowCellPos);
            _dungeonInfo.river.Add(nowCellPos);
        }
    }
    private void CreateRiver(Vector3Int start, Vector3Int end)
    {
        _dungeonInfo.riverstart.Add(start);
        _dungeonInfo.riverend.Add(end);
        float riverLength = Random.Range(_minRiverlength, _maxRiverLength);
        List<Vector3Int> river = new List<Vector3Int>();
        float distance = (start - end).magnitude;
        float weight;
        Vector3 nowPos = start;
        Vector3Int nowCellPos = start;
        Vector3 nowDir = Define.TileCoor8Dir.OrderBy(dir =>
            _noiseDic.TryGetValue(start + dir, out float noise)? noise:float.MaxValue).First();
        for(int i = 0;nowCellPos != end;i++)
        {
            weight = i / riverLength ;
            if(weight > 0.9f) { weight = 0.9f; }
            nowDir = Define.TileCoor8Dir.OrderBy(
                dir => Vector3.Dot(dir, nowDir) >= 0 && !river.Contains(Vector3Int.RoundToInt(nowPos + dir)) ?
                _noiseHelper.GetNoise(nowPos + dir) : 1)
                .First();
            nowDir = (nowDir.normalized * weight + (end - nowPos).normalized * (1 - weight)).normalized;
            nowPos += nowDir;
            nowCellPos = Vector3Int.RoundToInt(nowPos);
            if (_dungeonInfo.Board.TryGetValue(nowCellPos, out TileInfo tile)) { tile.Type = Define.TileType.Water; }
            else { _dungeonInfo.Board.Add(nowCellPos, new TileInfo(_world, Define.TileType.Water)); }
            river.Add(nowCellPos);
            _dungeonInfo.river.Add(nowCellPos);
        }
    }
    private IEnumerable<Vector3Int> CalculateRiverStartPos()
    {
        _startPosList = new List<Vector3Int>();
        int count = 0;
        foreach (KeyValuePair<Vector3Int, float> pair in _noiseDic)
        {
            if (pair.Value < _maxAltitude) { continue; }
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
        if (_dungeonInfo.Board.ContainsKey(Vector3Int.zero)) { _dungeonInfo.Board[Vector3Int.zero].Type = Define.TileType.Entrance; }
        else { _dungeonInfo.Board.Add(Vector3Int.zero,new TileInfo(_world,Define.TileType.Entrance)); }
        foreach(Vector3Int dir in Define.TileCoor8Dir)
        {
            if (!_dungeonInfo.Board.ContainsKey(dir)) { _dungeonInfo.Board.Add(dir,new TileInfo(_world)); }
        }
        while (_cellPosition == Vector3Int.zero)
        {
            _cellPosition = _dungeonInfo.Board.ElementAt(Random.Range(0, _dungeonInfo.Board.Count - 1)).Key;
        }
        _dungeonInfo.Board[_cellPosition].Type = Define.TileType.Exit;
        foreach (Vector3Int dir in Define.TileCoor8Dir)
        {
            if (!_dungeonInfo.Board.ContainsKey(_cellPosition + dir)) { _dungeonInfo.Board.Add(_cellPosition + dir, new TileInfo(_world)); }
        }
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
            if(pair.Value.Type == Define.TileType.Water) { _dungeonInfo.water.Add(pair.Key); }
        }
    }
}
