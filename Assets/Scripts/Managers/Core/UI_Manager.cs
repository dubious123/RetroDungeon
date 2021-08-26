using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UI_Manager
{
    Color _clickedCellColor;
    Vector3Int _clickedCellPos;
    public void Init()
    {

    }
    public void ResetTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos, Color.white);
    }
    public void ResetClickedCell()
    {
        Managers.GameMgr.Floor.SetColor(_clickedCellPos, _clickedCellColor);
    }
    public void PaintReachableEmptyTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos, Color.blue);
    }
    public void PaintReachableOccupiedTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos,Color.red);
    }
    public void PaintClickedCell(Vector3Int tilePos)
    {
        _clickedCellPos = tilePos;
        _clickedCellColor = Managers.GameMgr.Floor.GetColor(tilePos);
        Managers.GameMgr.Floor.SetColor(tilePos, Color.cyan);
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
