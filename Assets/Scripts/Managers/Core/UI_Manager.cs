using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UI_Manager
{
    Color _clickedCellColor;
    Vector3Int _clickedCellPos;
    public Game_DownPanel Canvas_Game_DownPanel;
    string _unitStatusBarName;
    public string UnitStatusBarName { get { return _unitStatusBarName; } }
    public void Init()
    {
        _unitStatusBarName = "UnitStatusBar";
        Managers.PoolMgr.CreatePool(Managers.ResourceMgr.Load<GameObject>($"Prefabs/UI/{_unitStatusBarName}"), 1);
        Canvas_Game_DownPanel = GameObject.Find("Canvas_Game").GetComponentInChildren<Game_DownPanel>();
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
    internal void PaintInRangeUnitTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos, Color.magenta);
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
