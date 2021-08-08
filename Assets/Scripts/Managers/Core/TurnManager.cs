using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class TurnManager
{
    #region Player
    PlayerController _playerController;
    Define.Turn _currentTurn;
    Define.UnitState _currentPlayerState;
    public Define.Turn CurrentTurn { get { return _currentTurn; } }
    public Define.UnitState CurrentPlayerState { get { return _currentPlayerState; } }
    #endregion

    #region Enemy

    #endregion
    public void Init()
    {
        _currentTurn = Define.Turn.Player;
        _currentPlayerState = Define.UnitState.Idle;
    }
    public void SetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }
    public void UpdatePlayerState(Define.UnitState nextState)
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
            case Define.UnitState.Idle:
                _playerController.HandleIdle();
                break;
            case Define.UnitState.Moving:
                _playerController.HandleMoving().RunCoroutine();
                break;
            case Define.UnitState.Skill:
                _playerController.HandleSkill();
                break;
            case Define.UnitState.UseItem:
                _playerController.HandleUseItem();
                break;
            case Define.UnitState.Die:
                _playerController.HandleDie();
                break;
            default:
                Debug.LogError("Not Defined PlayerState");
                break;
        }
    }
    void HandleEnemyTurn()
    {
        Debug.Log("EnemyTurn");
        Debug.Log("EnemyTurn End");
        UpdateTurn(Define.Turn.Player);
    }


}
