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
        set { TileGenerator.UpdateTile(this, value); _tileType = value; }
    }
    public int LeaveCost { get { return _leaveCost; } set { _leaveCost = value; } }

    public GameObject Unit { get { return _unit; } set { _unit = value; } }

    public UnityEvent TileInterEvent { get; set; }
    public UnityEvent TileExitEvent { get; set; }
    public TileInfo(int x, int y, Define.TileType tileType = Define.TileType.Empty)
    {
        Coor = new Vector3Int(x, y, 0);
        _tileType = tileType;
        TileInterEvent = new UnityEvent();
        TileExitEvent = new UnityEvent();
    }
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

