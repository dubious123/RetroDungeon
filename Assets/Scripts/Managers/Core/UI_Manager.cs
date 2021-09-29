using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UI_Manager
{
    Vector3Int _clickedCellPos;
    Game_DownPanel _game_DownPanel;
    TileOverlay[] _overlayArr;
    TileOverlay _floorOverlay;
    TileOverlay _unitOverlay;
    TileOverlay _moveOverlay;
    TileOverlay _skillOverlay;
    float _blinkf = 2 * Mathf.PI * 0.5f;
    Color _clickCellColor;
    Color[] _saving;
    public Game_DownPanel Canvas_Game_DownPanel
    {
        get
        {
            if (_game_DownPanel == null)
            {
                _game_DownPanel = GameObject.Find("Canvas_Game").GetComponentInChildren<Game_DownPanel>();
            }
            return _game_DownPanel;
        }
    }
    string _unitStatusBarName;
    public string UnitStatusBarName { get { return _unitStatusBarName; } }
    public void Init()
    {
        
        _clickCellColor = new Color32(83, 125, 170, 1);
        _overlayArr = new TileOverlay[4];
        _floorOverlay = new TileOverlay(Color.white);
        _moveOverlay = new TileOverlay(Color.blue);
        _unitOverlay = new TileOverlay(Color.red);
        _skillOverlay = new TileOverlay(Color.yellow);
        _overlayArr[0] = _floorOverlay;
        _floorOverlay.IsActive = true;
        _overlayArr[1] = _moveOverlay;
        _overlayArr[2] = _unitOverlay;
        _overlayArr[3] = _skillOverlay;
        _saving = new Color[_overlayArr.Length];
        _unitStatusBarName = "UnitStatusBar";
        Managers.PoolMgr.CreatePool(Managers.ResourceMgr.Load<GameObject>($"Prefabs/UI/{_unitStatusBarName}"), 1);
        Managers.PoolMgr.CreatePool(Managers.ResourceMgr.Load<GameObject>($"Prefabs/UI/{"TilePopup"}"), 1);
    }
    public void ResetFloorOverlay()
    {
        _floorOverlay.SetTilePos(Managers.GameMgr.CurrentDungeon.GetAllTiles());
    }
    void DisplayOverlay()
    {
        //foreach (TileOverlay overlay in _overlayArr.Where(overlay => !overlay.IsActive))
        //{
        //    ResetTile(overlay);
        //}
        foreach (TileOverlay overlay in _overlayArr.Where(overlay => overlay.IsActive))
        {
            overlay.Display();
        }
    }
    public void ShowOverlay(params Define.TileOverlay[] types)
    {
        foreach(Define.TileOverlay type in types)
        {
            _overlayArr[(int)type].IsActive = true;
        }
        DisplayOverlay();
    }
    public void HideOverlay(params Define.TileOverlay[] types)
    {
        foreach (Define.TileOverlay type in types)
        {
            _overlayArr[(int)type].IsActive = false;
        }
        DisplayOverlay();
    }
    public void HideAllOverlay()
    {
        //Todo 

        foreach(TileOverlay overlay in _overlayArr)
        {
            overlay.IsActive = false;
        }
        _floorOverlay.IsActive = true;
        DisplayOverlay();
    }
    public void UpdateTileSet<T>(Define.TileOverlay type,T vectors) where T : IEnumerable<Vector3Int>
    {
        _overlayArr[(int)type].SetTilePos(vectors);
    }
    public void AddTileSet<T>(Define.TileOverlay type, T vectors, Color? color = null) where T : IEnumerable<Vector3Int>
    {
        _overlayArr[(int)type].AddTilePos(vectors, color);
    }
    public void AddTileSet(Define.TileOverlay type, Vector3Int pos, Color? color = null)
    {
        _overlayArr[(int)type].AddTilePos(pos, color);
    }
    public void RemoveTileSet<T>(Define.TileOverlay type, T vectors) where T : IEnumerable<Vector3Int>
    {
        _overlayArr[(int)type].RemoveTilePos(vectors);
    }
    public void RemoveTileSet(Define.TileOverlay type, Vector3Int pos)
    {
        _overlayArr[(int)type].RemoveTilePos(pos);
    }
    public void MoveTileSet(Define.TileOverlay type,Vector3Int now, Vector3Int next, Color? color = null)
    {
        _overlayArr[(int)type].RemoveTilePos(now);
        _overlayArr[(int)type].AddTilePos(next, color);
    }
    public void UpdateClickCell(Vector3Int newPos)
    {
        _clickedCellPos = newPos;
    }
    public void StartDisplayClickCell(Vector3Int newPos)
    {
        _clickedCellPos = newPos;
        _StartDisplayClickCell().RunCoroutine("Display ClickedCell");
    }
    private IEnumerator<float> _StartDisplayClickCell()
    {
        float delta = 0f;
        for (int i = 0; i < _overlayArr.Length; i++)
        {
            if (_overlayArr[i].IsActive && _overlayArr[i].TileColorDict.ContainsKey(_clickedCellPos))
            {
                _saving[i] = _overlayArr[i].TileColorDict[_clickedCellPos];
            }
        }
        while (true) 
        {
            for (int i = 0; i < _overlayArr.Length; i++)
            {
                if (_overlayArr[i].IsActive && _overlayArr[i].TileColorDict.ContainsKey(_clickedCellPos))
                {
                    _overlayArr[i].TileColorDict[_clickedCellPos] = new Color(_clickCellColor.r, _clickCellColor.g, _clickCellColor.b, 0.7f + 0.3f * Mathf.Cos(delta * _blinkf));
                }
            }
            delta += 0.1f;
            DisplayOverlay();
            yield return Timing.WaitForSeconds(0.1f);
        }
    }
    public void EndDisplayClickCell()
    {
        if(Timing.KillCoroutines("Display ClickedCell") == 0) { return; }
        for(int i = 0; i < _overlayArr.Length; i++)
        {
            if(_overlayArr[i].IsActive && _overlayArr[i].TileColorDict.ContainsKey(_clickedCellPos))
            {
                _overlayArr[i].TileColorDict[_clickedCellPos] = _saving[i];
            }
        }
        DisplayOverlay();
    }
    public void ResetTile(Vector3Int tilePos)
    {
        Managers.GameMgr.Floor.SetColor(tilePos, Color.white);
    }
    public void ResetTile(TileOverlay overlay)
    {
        foreach(Vector3Int pos in overlay.TileColorDict.Keys)
        {
            ResetTile(pos);
        }
    }
    public void InitPlayerStatusBar(PlayerData playerData)
    {
        GameObject.Find("Status").GetComponent<PlayerStatus>().Init(playerData);
    }

    public void ShowTilePopup(Vector3Int pos,GameObject unit = null)
    {
        Managers.ResourceMgr.Instantiate("UI/TilePopup").GetComponentInChildren<TilePopup_Content>(true).Init(pos,unit);
    }
    public void Clear()
    {

    }
}
