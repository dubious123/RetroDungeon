using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonInfo : MonoBehaviour
{
    GameObject _dungeon;
    Transform[] _GridLayers;
    Tilemap[] _tilemaps;

    Define.World _world;
    Define.RoomSize _size;
    Define.MapType _mapType;
    Define.TerrainType _terrain;

    [SerializeField]
    int _roomSize;
    [SerializeField]
    int _iteration;
    [SerializeField]
    int _tileCount;
    [SerializeField]
    int _lakeCount;
    [SerializeField]
    int _riverCount;
    int _enemyCount;
    

    public Transform[] GridLayers { get { return _GridLayers; } }
    public Tilemap[] Tilemaps { get { return _tilemaps; } }
    public int Iteration { get { return _iteration; } }
    public int RoomSize { get { return _roomSize; } }
    public int TileCount { get { return _tileCount; } }
    public int LakeCount { get { return _lakeCount; } set { _lakeCount = value; } }
    public int RiverCount { get { return _riverCount; } set { _riverCount = value; } }
    public int EnemyCount { get { return _enemyCount; } }

    Dictionary<Vector3Int, TileInfo> _board;
    public Dictionary<Vector3Int, TileInfo> Board { get { return _board; } }

    public void Init(Define.World world, Define.RoomSize size = Define.RoomSize.Default, 
        Define.MapType mapType = Define.MapType.Default, 
        Define.TerrainType terrain = Define.TerrainType.Default)
    {
        _dungeon = gameObject;
        _world = world;
        _size = size == Define.RoomSize.Default ? size : (Define.RoomSize)Random.Range(0, (int)Define.RoomSize.num);
        _mapType = mapType;
        _terrain = terrain == Define.TerrainType.Default ? terrain : (Define.TerrainType)Random.Range(0, (int)Define.TerrainType.num);
        _board = new Dictionary<Vector3Int, TileInfo>();

        _GridLayers = _dungeon.GetChildren();

        _tilemaps = new Tilemap[Define.TileLayerNum];
        //Todo
        _lakeCount = 3;
        _riverCount = 3;
        for (int i = 0; i < Define.TileLayerNum; i++)
        {
            //???????????????????????
            _tilemaps[i] = _GridLayers[i].gameObject.GetOrAddComponent<Tilemap>();
        }


        switch (_world)
        {
            case Define.World.Unknown:
                break;
            case Define.World.AbandonedMineShaft:
                break;
        }
        switch (size)
        {
            case Define.RoomSize.Default:
                _roomSize = 500;
                _iteration = 5;
                break;
            case Define.RoomSize.Tiny:
                _roomSize = 200;
                _iteration = 5;
                break;
            case Define.RoomSize.Small:
                _roomSize = 350;
                _iteration = 5;
                break;
            case Define.RoomSize.Big:
                _roomSize = 650;
                _iteration = 5;
                break;
            case Define.RoomSize.Huge:
                _roomSize = 800;
                _iteration = 5;
                break;
        }
        switch (_mapType)
        {

        }
        switch (_terrain)
        {
            case Define.TerrainType.Default:
                break;
            case Define.TerrainType.Scattered:
                _iteration *= 3;
                _roomSize /= 3;
                break;
            case Define.TerrainType.Centered:
                _iteration /= 3;
                _roomSize *= 3;
                break;
            case Define.TerrainType.Cliff:
                _iteration *= 3;
                _roomSize /= 3;
                break;
        }

        _tileCount = _iteration * _roomSize;
        _enemyCount = _roomSize / (_iteration*10);
    }


}
