using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator
{
    public static void UpdateTile(TileInfo tile, Define.TileType newType)
    {
        if(tile.Type == newType) { return; }
        switch (newType)
        {
            //Todo
            case Define.TileType.Default: 
                break;
            case Define.TileType.Water:
                tile.LeaveCost = 1;
                break;
            case Define.TileType.Lava:
                tile.LeaveCost = 1;
                break;
            case Define.TileType.Entrance:
                break;
            case Define.TileType.Exit:
                tile.TileInterEvent.AddListener(() => TileEvents.EnableExit(tile.Unit));
                tile.TileExitEvent.AddListener(() => TileEvents.DisableExit(tile.Unit));
                break;
            default:
                break;
        }
    }
}
