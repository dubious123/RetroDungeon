using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonRenderer
{
    static BaseTileBrush _tileBrush;
    public static void RenderDungeon(Dungeon dungeon)
    {
        ClearTiles();
        SetBrush(dungeon.ID.World);
        BoundsInt posSet = new BoundsInt(0,0,0,dungeon.Width,dungeon.Hight,1);
        foreach (Vector3Int nowpos in posSet.allPositionsWithin)
        {
            _tileBrush.Paint(nowpos, dungeon.GetTile(nowpos));
        }
    }
    static void ClearTiles()      
    {
        foreach (Tilemap tilemap in Managers.WorldMgr.World_go.GetComponentsInChildren<Tilemap>())
        {
            tilemap.ClearAllTiles();
        }
    }
    static void SetBrush(Define.World world)
    {
        switch (world)
        {
            case Define.World.Unknown:
                break;
            case Define.World.AbandonedMineShaft:
                _tileBrush = new TileBrush_AbandonedMineShaft();
                break;
            case Define.World.CrystalLake:
                _tileBrush = new TileBrush_CrystalLake();
                break;
            default:
                break;

        }
    }
}
