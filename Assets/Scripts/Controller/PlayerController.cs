using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction.CallbackContext _clickContext;
    Define.PlayerState _state;
    Define.Turn _currentTurn;


    void Init()
    {      
        _state = Define.PlayerState.Idle;

        _currentTurn = Managers.TurnMgr.CurrentTurn;
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _playerInput.DeactivateInput();
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if (Managers.TurnMgr.CurrentTurn != Define.Turn.Player)
        {
            if (_playerInput.inputIsActive == true) { _playerInput.DeactivateInput(); }
        }
        else //Player Turn
        {
            if (_playerInput.inputIsActive == false) { _playerInput.ActivateInput(); } 

            //Todo : 지금은 이동만 한다고 가정하자
        }
    }
    private void UpdateIdle()
    {
        Vector2 mouseScreenPos = _playerInput.actions.FindAction("MousePosition").ReadValue<Vector2>();
        Vector3Int? clickedCellPos = Managers.InputMgr.GetClickedCellPosition(mouseScreenPos);
        if(clickedCellPos.HasValue == false) { return; }
        gameObject.transform.position = transform.parent.GetComponent<Tilemap>().GetCellCenterWorld(clickedCellPos.Value);

        

    }
    private void UpdateMoving()
    {

    }
    private void UpdateSkill()
    {
    }
    private void UpdateItem()
    {
    }
    private void UpdateDie()
    {
    }

    public void OnClicked(InputAction.CallbackContext context)
    {

        switch (_state)
        {
            case Define.PlayerState.Idle:
                UpdateIdle();
                break;
            case Define.PlayerState.Moving:
                UpdateMoving();
                break;
            case Define.PlayerState.Skill:
                break;
            case Define.PlayerState.UseItem:
                break;
            case Define.PlayerState.Die:
                break;
            default:
                break;
        }
    }



}
