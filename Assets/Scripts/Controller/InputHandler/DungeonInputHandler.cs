using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DungeonInputHandler : MonoBehaviour, Imouse
{
    PlayerController _playerController;
    Vector3Int? _mouseCellPos;
    public void Init()
    {
        _playerController = Managers.GameMgr.Player.GetComponent<PlayerController>();
    }
    public void OnMouseMove(InputAction.CallbackContext context)
    {

    }
    public void OnMouseDown(InputAction.CallbackContext context)
    {
        Vector2 mosuePos = Managers.InputMgr.MouseScreenPosition;
        Vector3Int? mouseCellPos = Managers.InputMgr.GetMouseCellPos(mosuePos);
        if (mouseCellPos.HasValue && _playerController.ReachableEmptyTileDict.ContainsKey(mouseCellPos.Value))
        {
            _playerController.CurrentMouseCellPos = mouseCellPos;
            //Todo
            if (Managers.DungeonMgr.GetTileInfoDict()[mouseCellPos.Value].Occupied)
            {
                //Todo
                return;
            }
            _playerController.UpdatePlayerState(Define.UnitState.Moving);
        }
    }

    public void OnDrag(InputAction.CallbackContext context)
    {

    }
    public void OnMouseUp(InputAction.CallbackContext context)
    {

    }
    public void OnMouseHover(InputAction.CallbackContext context)
    {
        _mouseCellPos = Managers.InputMgr.GetMouseCellPos(context.ReadValue<Vector2>());
        if (_mouseCellPos.HasValue)
        {
            _playerController.UpdatePath(_mouseCellPos.Value);
        }
    }
}
