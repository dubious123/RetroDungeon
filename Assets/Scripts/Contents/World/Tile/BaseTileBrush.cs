using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class BaseTileBrush 
{
    protected Tilemap[] _tilemaps;
    protected List<IsometricRuleTile[]> _tilesList;
    protected IsometricRuleTile[] _ruleTiles_Empty;
    protected IsometricRuleTile[] _ruleTiles_Default;
    protected IsometricRuleTile[] _ruleTiles_Water;
    protected IsometricRuleTile[] _ruleTiles_Lava;
    protected IsometricRuleTile[] _ruleTiles_Entrance;
    protected IsometricRuleTile[] _ruleTiles_Exit;
    public BaseTileBrush()
    {
        _tilemaps = Managers.WorldMgr.World_go.GetComponentsInChildren<Tilemap>();
        _tilesList = new List<IsometricRuleTile[]>
        {_ruleTiles_Empty, _ruleTiles_Default,_ruleTiles_Water,_ruleTiles_Lava,_ruleTiles_Entrance,_ruleTiles_Exit};
        int length = _tilemaps.Length;
        _ruleTiles_Empty = new IsometricRuleTile[length];
        _ruleTiles_Default = new IsometricRuleTile[length];
        _ruleTiles_Water = new IsometricRuleTile[length];
        _ruleTiles_Lava = new IsometricRuleTile[length];
        _ruleTiles_Entrance = new IsometricRuleTile[length];
        _ruleTiles_Exit = new IsometricRuleTile[length];
        LoadData();
    }
    public virtual void Paint(Vector3Int pos, TileInfo tile)
    {
        switch (tile.Type)
        {
            case Define.TileType.Empty:
                PaintEmpty(pos);
                break;
            case Define.TileType.Default:
                PaintDefault(pos);
                break;
            case Define.TileType.Water:
                PaintWater(pos);
                break;
            case Define.TileType.Lava:
                PaintLava(pos);
                break;
            case Define.TileType.Entrance:
                PaintEntrance(pos);
                break;
            case Define.TileType.Exit:
                PaintExit(pos);
                break;
            default:
                break;

        }
    }
    protected virtual void PaintEmpty(Vector3Int pos)
    {
        _tilemaps.SetTile(pos,_ruleTiles_Empty);
    }
    protected virtual void PaintDefault(Vector3Int pos)
    {
        _tilemaps.SetTile(pos, _ruleTiles_Default);

    }
    protected virtual void PaintWater(Vector3Int pos)
    {
        _tilemaps.SetTile(pos, _ruleTiles_Water);

    }
    protected virtual void PaintLava(Vector3Int pos)
    {
        _tilemaps.SetTile(pos, _ruleTiles_Lava);

    }
    protected virtual void PaintEntrance(Vector3Int pos)
    {
        _tilemaps.SetTile(pos, _ruleTiles_Entrance);

    }
    protected virtual void PaintExit(Vector3Int pos)
    {
        _tilemaps.SetTile(pos, _ruleTiles_Exit);
    }
    protected abstract void LoadData();
}
