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
    Dictionary<Vector3Int, EnemyData> _enemyDic;
    SimplePriorityQueue<EnemyData, int> _enemyQueue;
    EnemyData _currentEnemyData;
    EnemyController _currentEnemyController;
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
        _enemyDic = _currentPool.EnemyDic;
        _enemyQueue = _currentPool.EnemyQueue;
    }
    public void UpdatePlayerState(Define.UnitState nextState)
    {
        _currentPlayerState = nextState;
        UpdateTurn(_currentTurn);
    }
    public void UpdateEnemyState()
    {
        if(_enemyQueue.Count == 0) { UpdateTurn(Define.Turn.Player); }
        else 
        {
            _currentEnemyData = _enemyQueue.Dequeue();
            _currentEnemyController = _currentEnemyData.GetComponent<EnemyController>();
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
    public void HandleEnemyTurn()
    {
        Debug.Log("EnemyTurn");
        switch (_currentEnemyData.CurrentState)
        {
            case Define.UnitState.Idle:
                _currentEnemyController.HandleIdle();
                break;
            case Define.UnitState.Moving:
                _currentEnemyController.HandleMoving().RunCoroutine();
                break;
            case Define.UnitState.Skill:
                _currentEnemyController.HandleSkill();
                break;
            case Define.UnitState.UseItem:
                _currentEnemyController.HandleUseItem();
                break;
            case Define.UnitState.Die:
                _currentEnemyController.HandleDie();
                break;
            default:
                break;
        }
        Debug.Log("EnemyTurn End");
    }


}
