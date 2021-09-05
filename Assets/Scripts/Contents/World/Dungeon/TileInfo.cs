using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileInfo
{
    Define.World _worldtype = Define.World.Unknown;
    Define.TileType _tileType;
    RuleTile[] _tiles;
    int _level = 0;
    int _leaveCost;

    GameObject _unit;
    bool _occupied;

    public Define.TileType Type { get { return _tileType; } set { _tileType = value; } }
    public RuleTile[] RuleTiles { get { return _tiles; } }
    public int Level { get { return _level; } }
    public int LeaveCost { get { return _leaveCost; } }

    public GameObject Unit { get { return _unit; } set { _unit = value; } }
    public bool Occupied { get { return _occupied; } set { _occupied = value; } }
    public TileInfo(Define.World world, Define.TileType tileType = Define.TileType.Default)
    {
        Init(world, tileType);
    }
    public void Init(Define.World world,Define.TileType tileType)
    {
        _worldtype = world;
        _tileType = tileType;
        _tiles = new RuleTile[Define.TileLayerNum];
        _occupied = false;
    }
    public void SetTileDetails() 
    {
        _tiles[0] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/Floor");
        switch (_tileType)
        {
            //Todo
            case Define.TileType.Default:
                _tiles[1] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/DefaultTile");
                if (Random.Range(0, 3) == 0) { _tiles[4] = Managers.ResourceMgr.Load<Overlay1>("Prefabs/Tiles/AbandonedMineShaft/Overlay_1"); }
                break;
            case Define.TileType.Water:
                _tiles[2] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/Water_Complex");
                _leaveCost = 1;
                break;
            case Define.TileType.Entrance:
                _tiles[1] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/DefaultTile");
                _tiles[3] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/Entrance");
                _level++;           
                break;
            case Define.TileType.Exit:
                _tiles[1] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/Exit");
                break;
            default:
                break;
        }
    }
    public void SetUnit(GameObject unit) 
    {
        _occupied = true;
        _unit = unit;
    }
    public void RemoveUnit()
    {
        _occupied = false;
        _unit = null;
    }
}

