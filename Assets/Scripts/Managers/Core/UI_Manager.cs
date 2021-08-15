using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UI_Manager
{
    public void Init()
    {

    }
    public void PaintReachableEmptyTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos, Color.blue);
    }
    public void ResetReachableTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos, Color.white);
    }
    public void PaintReachableOccupiedTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos,Color.red);
    }
    public void ResetReachableOccupiedTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos, Color.white);
    }
}
