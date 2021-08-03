using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager
{
    PlayerController _playerController;
    Define.Turn _currentTurn;
    Define.PlayerState _currentPlayerState;
    public Define.Turn CurrentTurn { get { return _currentTurn; } }
    public Define.PlayerState CurrentPlayerState { get { return _currentPlayerState; } }
    public void Init()
    {
        _currentTurn = Define.Turn.Player;
        _currentPlayerState = Define.PlayerState.Idle;
    }
    public void SetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }
    public void UpdatePlayerState(Define.PlayerState nextState)
    {
        _currentPlayerState = nextState;
        UpdateTurn(_currentTurn);
    }
    public void UpdateTurn(Define.Turn nextTurn)
    {
        _currentTurn = nextTurn;
        switch (nextTurn)
        {
            case Define.Turn.Player:
                HandlePlayerTurn();
                break;
            case Define.Turn.Enemy:
                HandleEnemyTurn();
                break;
            default:
                break;
        }
    }

    void HandlePlayerTurn()
    {
        switch (_currentPlayerState)
        {
            case Define.PlayerState.Idle:
                _playerController.HandleIdle();
                break;
            case Define.PlayerState.Moving:
                _playerController.HandleMoving();
                break;
            case Define.PlayerState.Skill:
                _playerController.HandleSkill();
                break;
            case Define.PlayerState.UseItem:
                _playerController.HandleUseItem();
                break;
            case Define.PlayerState.Die:
                _playerController.HandleDie();
                break;
            default:
                Debug.LogError("Not Defined PlayerState");
                break;
        }
    }
    void HandleEnemyTurn()
    {

    }
    public void SetTurn()
    {
        if (_currentTurn == Define.Turn.Player)
        {
            if (Managers.GameMgr.Player_Data.MaxAp > 0)
            {

            }
            else
            {
                _currentTurn = Define.Turn.Enemy;
            }
        }
        else if (_currentTurn == Define.Turn.Enemy)
        {

        }
    }

}
