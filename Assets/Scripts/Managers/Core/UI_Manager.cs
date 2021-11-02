using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UI_Manager
{
    PopupController _popupController;
    Game_DownPanel _downPanel;
    GameObject _popup_PlayerInfo;
    Vector3Int _clickedCellPos;
    TileOverlay[] _overlayArr;
    TileOverlay _floorOverlay;
    TileOverlay _unitOverlay;
    TileOverlay _moveOverlay;
    TileOverlay _skillOverlay;
    float _blinkf = 2 * Mathf.PI * 0.5f;
    Color _clickCellColor;
    Color[] _saving;
    public PopupController Popup_Controller
    {
        get
        {
            if (_popupController == null)
            {
                _popupController = Managers.ResourceMgr.Instantiate("UI/Popup/PopupController").GetComponent<PopupController>();
            }
            return _popupController;
        }
    }
    public Game_DownPanel DownPanel
    {
        get
        {
            if (_downPanel == null)
            {
                _downPanel = GameObject.FindWithTag("UI_DownPanel")?.GetComponent<Game_DownPanel>();
            }
            return _downPanel;
        }
    }
    public Popup_PlayerInfo _Popup_PlayerInfo { get { return _popup_PlayerInfo.GetComponent<Popup_PlayerInfo>(); } }
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
        _popup_PlayerInfo = GameObject.FindWithTag("UI_Popups_Parent").transform.Find("Popup_PlayerInfo").gameObject;
    }
    public void ResetFloorOverlay()
    {
        _floorOverlay.SetTilePos(Managers.GameMgr.CurrentDungeon.GetAllTiles());
    }
    void DisplayOverlay()
    {
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
    public void InitGamePopups()
    {
        Popup_Controller.Init_GamePopups();

        Managers.InputMgr.GameController.PlayerInfoEvent.AddListener(() => ShowPopup_PlayerInfo());
    }
    public void InitPlayerStatusBar(PlayerData playerData)
    {
        GameObject.FindWithTag("UI_Status").GetComponent<PlayerStatus>().Init(playerData);
    }

    public void ShowPopup_TileInfo(Vector3Int pos,GameObject unit = null)
    {
        Managers.ResourceMgr.Instantiate(Popup_Controller._TileInfo).GetComponentInChildren<TilePopup_Content>(true).Init(pos, unit);
    }
    public GameObject ShowGetSkillPopup()
    {
        return Managers.ResourceMgr.Instantiate(Popup_Controller._GetSkill);
    }
    public GameObject ShowPopup_UnitStatus()
    {
        return Managers.ResourceMgr.Instantiate(Popup_Controller._UnitStatusBar, GameObject.FindWithTag("UI_Game_MainCanvas").transform);
    }
    public GameObject ShowPopup_PlayerDeath()
    {
        return Managers.ResourceMgr.Instantiate(Popup_Controller._PlayerDeath);
    }
    public void ShowPopup_PlayerInfo()
    {
        _popup_PlayerInfo.SetActive(true);
    }
    public void Clear()
    {
        _popupController.Clear();
        _popupController = null;
        _downPanel = null;
    }
}
