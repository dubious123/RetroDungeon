using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileInfo
{
    public Vector3Int Coor;
    public WorldPosition PreWorld;
    public WorldPosition NextWorld;


    Define.TileType _tileType;

    GameObject _unit;
    int _leaveCost;


    public Define.TileType Type
    {
        get { return _tileType; }
        set { TileGenerator.UpdateTile(this, value); }
    }
    public int LeaveCost { get { return _leaveCost; } set { _leaveCost = value; } }

    public GameObject Unit { get { return _unit; } set { _unit = value; } }

    public UnityEvent TileInterEvent { get; set; }
    public UnityEvent TileExitEvent { get; set; }
    public TileInfo(Define.TileType tileType = Define.TileType.Empty)
    {
        _tileType = tileType;
        TileInterEvent = new UnityEvent();
        TileExitEvent = new UnityEvent();
    }
    //public void SetTileDetails() 
    //{
    //    _tiles[0] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/Floor");
    //    switch (_tileType)
    //    {
    //        //Todo
    //        case Define.TileType.Default:
    //            _tiles[1] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/DefaultTile");
    //            if (Random.Range(0, 3) == 0) { _tiles[4] = Managers.ResourceMgr.Load<Overlay1>("Prefabs/Tiles/AbandonedMineShaft/Overlay_1"); }
    //            break;
    //        case Define.TileType.Water:
    //            _tiles[2] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/Water_Complex");
    //            _leaveCost = 1;
    //            break;
    //        case Define.TileType.Entrance:
    //            _tiles[1] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/DefaultTile");
    //            _tiles[3] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/Entrance");
    //            _level++;           
    //            break;
    //        case Define.TileType.Exit:
    //            _tiles[1] = Managers.ResourceMgr.Load<IsometricRuleTile>("Prefabs/Tiles/AbandonedMineShaft/Exit");
    //            TileInterEvent.AddListener(() => Managers.DungeonMgr.CurrentDungeon.GetComponent<TileEvents>().EnableExit(_unit));
    //            TileExitEvent.AddListener(() => Managers.DungeonMgr.CurrentDungeon.GetComponent<TileEvents>().DisableExit(_unit));
    //            break;
    //        default:
    //            break;
    //    }
    //}
    public void SetUnit(GameObject unit) 
    {
        _unit = unit;
        TileInterEvent.Invoke();
    }
    public void RemoveUnit()
    {
        TileExitEvent.Invoke();
        _unit = null;
        
    }

}

