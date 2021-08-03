using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileInfo
{
    Define.World _worldtype = Define.World.Unknown;
    Vector3Int _position;
    Define.TileType _tileType;
    IsometricRuleTile[] _tiles;
    int _level = 0;
    int _leaveCost;

    public Define.TileType tileType { get { return _tileType; } set { _tileType = value; SetTileType(); } }
    public IsometricRuleTile[] RuleTiles { get { return _tiles; } }

    Vector3Int Position { get { return _position; } }
    public int Level { get { return _level; } }
    public int LeaveCost { get { return _leaveCost; } }

    public TileInfo(Define.World world, Define.TileType tileType = Define.TileType.Default)
    {
        Init(world, tileType);
    }
    public void Init(Define.World world,Define.TileType tileType)
    {
        _worldtype = world;
        _tileType = tileType;
        _tiles = new IsometricRuleTile[Define.TileLayerNum];
        SetTileType();

    }
    void SetTileType()
    {
        switch (_tileType)
        {
            //Todo
            case Define.TileType.Default:
                _tiles[0] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/Floor");
                _tiles[1] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/DefaultTile");

                _leaveCost = 0;
                break;
            case Define.TileType.Entrance:
                _tiles[0] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/Floor");
                _tiles[1] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/DefaultTile");
                _tiles[2] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/Entrance");
                _level++;
                
                _leaveCost = 0;
                break;
            case Define.TileType.Exit:
                _tiles[0] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/Floor");
                _tiles[1] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/Exit");

                _leaveCost = 0;
                break;
            default:
                break;
        }
    }
}

