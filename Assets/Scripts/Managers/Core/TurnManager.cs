using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Priority_Queue;

public class TurnManager
{
    Define.Turn _currentTurn;
    public Define.Turn CurrentTurn { get { return _currentTurn; } }

    #region Player
    PlayerController _playerController;
    #endregion

    #region Unit
    SpawningPool _currentPool;
    List<UnitData> _unitList;
    UnitData _currentUnitData;
    UnitController _currentUnitController;
    private class UnitPriorityComparer : IComparer<int>
    {
        public int Compare(int xSpeed, int ySpeed)
        {
            if (xSpeed > ySpeed) { return ySpeed; }
            else { return xSpeed; }
        }
    }
    SimplePriorityQueue<UnitData, int> _unitQueue;
    #endregion
    public void Init()
    {
        _currentTurn = Define.Turn.Player;
        _unitQueue = new SimplePriorityQueue<UnitData, int>(new UnitPriorityComparer());

    }

    public void GetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }
    public void UpdateDataFromCurrentSpawningPool()
    {
        _currentPool = Managers.DungeonMgr.CurrentDungeon.GetComponent<SpawningPool>();
        _unitList = _currentPool.UnitList;
    }
    public void HandlePlayerTurn(Define.UnitState nextState = Define.UnitState.Idle)
    {
        if(_currentTurn == Define.Turn.Enemy) { _currentTurn = Define.Turn.Player; }
        Managers.CameraMgr.GameCamController.Target = _playerController.gameObject;
        switch (nextState)
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
    public void _HandleUnitTurn()
    {
        if(_currentTurn == Define.Turn.Player) { _currentTurn = Define.Turn.Enemy; ResetUnitQueue(); }
        if(!_unitQueue.TryDequeue(out _currentUnitData)) { HandlePlayerTurn(); return; }
        Managers.CameraMgr.GameCamController.Target = _currentUnitData.gameObject;
        Timing.WaitForSeconds(0.5f);
        _currentUnitController = _currentUnitData.GetComponent<UnitController>();
        _currentUnitController._PerformUnitTurn().RunCoroutine();
    }
    private void ResetUnitQueue()
    {
        _unitQueue.Clear();
        foreach(UnitData unitData in _unitList)
        {
            _unitQueue.Enqueue(unitData);
        }
    }

}
