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
        Managers.UI_Mgr.EndDisplayClickCell();
        Managers.InputMgr.GameController.RightClickEvent.RemoveListener(_handler.Deactivate);
        Managers.InputMgr.GameController.RightClickEvent.AddListener(_handler.Deactivate);
        Vector3Int mouseCellPos = Managers.InputMgr.GetMouseCellPos();
        if (Managers.GameMgr.HasTile(mouseCellPos))  
        {
            Managers.UI_Mgr.StartDisplayClickCell(mouseCellPos);
            GameObject unit = Managers.GameMgr.GetUnit(mouseCellPos);
            if(unit != null) 
            {           
                if (_unitStatusBar == null || _unitStatusBar.activeInHierarchy == false)
                {
                    _unitStatusBar = Managers.ResourceMgr.Instantiate($"UI/{Managers.UI_Mgr.UnitStatusBarName}", _gameCanvas.transform);
                }
                _unitStatusBar.GetComponentInChildren<UnitStatus>().Init(unit.GetComponent<BaseUnitData>());
                _unitCam.SetPosition(unit.transform.position);
            }
            else { Managers.ResourceMgr.Destroy(_unitStatusBar); }

            if (_playerController.InRangeTileDict.ContainsKey(mouseCellPos)) 
            {
                _playerController.UpdateTargetPos(mouseCellPos);
                _handler.EnableBtns(true,unit);
                _handler.Yes.YesEvent.AddListener(() => 
                {
                    _playerController.UpdatePlayerState(Define.UnitState.Skill);
                    _skillCache.Skill = null;
                });
                _handler.Exit.ExitEvent.AddListener(() => _skillCache.Skill?.Cancel());
            }
            //Todo
            else if (_playerController.ReachableOccupiedCoorSet.Contains(mouseCellPos))
            {
                _handler.EnableBtns(false, unit);
            }
            else if (_playerController.ReachableEmptyTileDict.ContainsKey(mouseCellPos)) 
            {
                _playerController.UpdatePath(mouseCellPos);
                _handler.EnableBtns(true, unit);
                _handler.Yes.YesEvent.AddListener(() => _playerController.UpdatePlayerState(Define.UnitState.Moving));
                _handler.Exit.ExitEvent.AddListener(() => _skillCache.Skill?.Cancel());
            }
            else 
            {
                _handler.EnableBtns(false, unit);
            }
            ShowClickCircleUI(mouseCellPos);
            Managers.CameraMgr.GameCamController.TargetPos = Managers.GameMgr.Floor.GetCellCenterWorld(mouseCellPos);
        }
    }
    private void ShowClickCircleUI(Vector3Int pos)
    {
        _handler.SetPosition(pos);
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
