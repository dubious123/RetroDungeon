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
    public void ResetTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos, Color.white);
    }
    public void PaintReachableEmptyTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos, Color.blue);
    }
    public void PaintReachableOccupiedTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos,Color.red);
    }
    public void PaintInRangeTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos, Color.yellow);
    }
    public void InitPlayerStatusBar(PlayerData playerData)
    {
        GameObject.Find("Status").GetComponent<PlayerStatus>().Init(playerData);
    }
}
