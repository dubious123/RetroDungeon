using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DungeonInputHandler : MonoBehaviour, Imouse
{
    ClickCircleInputHandler _handler;
    PlayerController _playerController;
    GameObject _unitStatusBar;
    ActiveSkillCache _skillCache;
    GameObject _gameCanvas;
    UnitCamController _unitCam;
    public void Init()
    {
        _unitCam = GameObject.Find("Unit_Cam").GetComponent<UnitCamController>();
        _handler = GameObject.Find("MainCircle").GetComponent<ClickCircleInputHandler>();
        _gameCanvas = GameObject.Find("Canvas_Game");
        _skillCache = _gameCanvas.GetChildren()[1].GetComponent<ActiveSkillCache>();
        _playerController = Managers.GameMgr.Player_Controller;
    }
    public void OnMouseMove(InputAction.CallbackContext context)
    {

    }
    public void OnMouseDown(InputAction.CallbackContext context)
    {
        Managers.UI_Mgr.ResetClickedCell();
        Vector2 mousePos = Managers.InputMgr.MouseScreenPosition;
        Vector3Int? mouseCellPos = Managers.InputMgr.GetMouseCellPos(mousePos);
        if (mouseCellPos.HasValue)  
        {
            Managers.UI_Mgr.PaintClickedCell(mouseCellPos.Value);
            GameObject unit = Managers.DungeonMgr.GetTileInfoDict()[mouseCellPos.Value].Unit;
            if(unit != null) 
            {
                //_unitStatusBar = GameObject.Find(Managers.UI_Mgr.UnitStatusBarName);             
                if (_unitStatusBar == null || _unitStatusBar.activeInHierarchy == false)
                {
                    _unitStatusBar = Managers.ResourceMgr.Instantiate($"UI/{Managers.UI_Mgr.UnitStatusBarName}", _gameCanvas.transform);
                }
                _unitStatusBar.GetComponentInChildren<UnitStatus>().Init(unit.GetComponent<BaseUnitData>());
                _unitCam.SetPosition(unit.transform.position);
            }
            else { Managers.ResourceMgr.Destroy(_unitStatusBar); }

            if (_playerController.InRangeTileDict.ContainsKey(mouseCellPos.Value)) 
            {
                _playerController.UpdateTargetPos(mouseCellPos.Value);
                _handler.EnableBtns(true,unit);
                _handler.Yes.YesEvent.AddListener(() => 
                {
                    _playerController.UpdatePlayerState(Define.UnitState.Skill);
                    _skillCache.Skill = null;
                });
                _handler.Exit.ExitEvent.AddListener(() => _skillCache.Skill?.Cancel());
            }
            else if (_playerController.ReachableEmptyTileDict.ContainsKey(mouseCellPos.Value)) 
            {
                _playerController.UpdatePath(mouseCellPos.Value);
                _handler.EnableBtns(true, unit);
                _handler.Yes.YesEvent.AddListener(() => _playerController.UpdatePlayerState(Define.UnitState.Moving));
                _handler.Exit.ExitEvent.AddListener(() => _skillCache.Skill?.Cancel());
            }
            //Todo
            else if (_playerController.ReachableOccupiedCoorSet.Contains(mouseCellPos.Value)) 
            {
                _handler.EnableBtns(false, unit);
            }
            else 
            {
                _handler.EnableBtns(false, unit);
            }
            ShowClickCircleUI(mouseCellPos.Value);
            Managers.CameraMgr.GameCamController.TargetPos = Managers.GameMgr.Floor.GetCellCenterWorld(mouseCellPos.Value);
        }
    }
    private void ShowClickCircleUI(Vector3Int pos)
    {
        _handler.SetPosition(pos);
        //_handler.transform.position = Managers.GameMgr.Floor.GetCellCenterWorld(pos);
        _handler.Activate();
    }
    public void OnDrag(InputAction.CallbackContext context)
    {

    }
    public void OnMouseUp(InputAction.CallbackContext context)
    {

    }
    public void OnMouseHover(InputAction.CallbackContext context)
    {
    }
    public void DropDown() { }
    public void GetDrop(GameObject ogj) { }
}
