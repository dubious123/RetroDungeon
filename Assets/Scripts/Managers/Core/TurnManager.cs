using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Priority_Queue;

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
    SpawningPool _currentPool;
    SimplePriorityQueue<UnitData, int> _unitQueue;
    UnitData _currentUnitData;
    UnitController _currentUnitController;
    #endregion
    public void Init()
    {
        _currentTurn = Define.Turn.Player;
        _currentPlayerState = Define.UnitState.Idle;
    }

    public void GetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }
    public void UpdateDataFromCurrentSpawningPool()
    {
        _currentPool = Managers.DungeonMgr.CurrentDungeon.GetComponent<SpawningPool>();
        _unitQueue = _currentPool.EnemyQueue;
    }
    public void UpdatePlayerState(Define.UnitState nextState)
    {
        _currentPlayerState = nextState;
        UpdateTurn(_currentTurn);
    }
    public void UpdateUnit()
    {
        if(_unitQueue.Count == 0) { UpdateTurn(Define.Turn.Player); }
        else 
        {
            _currentUnitData = _unitQueue.Dequeue();
            _currentUnitController = _currentUnitData.GetComponent<UnitController>();
            UpdateTurn(Define.Turn.Enemy); 
        }
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
                HandleUnitTurn();
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
            case Define.UnitState.Die:
                _playerController.HandleDie();
                break;
            default:
                Debug.LogError("Not Defined PlayerState");
                break;
        }
    }
    public void HandleUnitTurn()
    {
        UpdateUnit();
        switch (_currentUnitData.CurrentState)
        {
            case Define.UnitState.Idle:
                _currentUnitController.HandleIdle();
                break;
            case Define.UnitState.Moving:
                _currentUnitController.HandleMoving().RunCoroutine();
                break;
            case Define.UnitState.Skill:
                _currentUnitController.HandleSkill();
                break;
            case Define.UnitState.Die:
                _currentUnitController.HandleDie();
                break;
            default:
                break;
        }
    }


}
