using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DungeonInputHandler : MonoBehaviour, Imouse
{
    ClickCircleInputHandler _handler;
    PlayerController _playerController;
    Vector3Int? _mouseCellPos;
    public void Init()
    {
        _handler = GameObject.Find("MainCircle").GetComponent<ClickCircleInputHandler>();
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
            if (_playerController.InRangeTileDict.ContainsKey(mouseCellPos.Value)) 
            {
                _handler.EnableBtns(true);
                _handler.Yes.YesEvent.AddListener(() => _playerController.UpdatePlayerState(Define.UnitState.Skill));
            }
            else if (_playerController.ReachableEmptyTileDict.ContainsKey(mouseCellPos.Value)) 
            {
                _playerController.UpdatePath(mouseCellPos.Value);
                _handler.EnableBtns(true);
                _handler.Yes.YesEvent.AddListener(() => _playerController.UpdatePlayerState(Define.UnitState.Moving));
            }
            //Todo
            else if (_playerController.ReachableOccupiedCoorSet.Contains(mouseCellPos.Value)) 
            {
                _handler.EnableBtns(false);
            }
            else 
            {
                _handler.EnableBtns(false);
            }
            ShowClickCircleUI(mouseCellPos.Value);
        }
    }
    private void ShowClickCircleUI(Vector3Int pos)
    {
        _handler.transform.position = Managers.CameraMgr.GameCam.WorldToScreenPoint(Managers.GameMgr.Floor.GetCellCenterWorld(pos));
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
