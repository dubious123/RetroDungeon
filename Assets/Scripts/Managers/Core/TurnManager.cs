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
    List<BaseUnitData> _unitList;
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
    public void UpdateCurrentUnitList()
    {
        _unitList = Managers.GameMgr.CurrentDungeon.UnitList;
    }
    public void HandlePlayerTurn(Define.UnitState nextState = Define.UnitState.Idle)
    {
        if(_playerController == null) { return; }
        if(_currentTurn == Define.Turn.Enemy) { _currentTurn = Define.Turn.Player; }
        Managers.CameraMgr.GameCamController.Target = _playerController.gameObject;
        Managers.UI_Mgr.DownPanel.ActivateAll();
        switch (nextState)
        {
            case Define.UnitState.Idle:
                _playerController.HandleIdle();
                break;
            case Define.UnitState.Moving:
                _playerController.HandleMoving().RunCoroutine();
                break;
            case Define.UnitState.Skill:
                _playerController.HandleSkill().RunCoroutine();
                break;
            case Define.UnitState.Die:
                _playerController._HandleDie();
                break;
            default:
                Debug.LogError("Not Defined PlayerState");
                break;
        }
    }
    public IEnumerator<float> _HandleUnitTurn()
    {
        if(_currentTurn == Define.Turn.Player) { _currentTurn = Define.Turn.Enemy; ResetUnitQueue(); }
        if(!_unitQueue.TryDequeue(out _currentUnitData)) { HandlePlayerTurn(); yield break; }
        Managers.CameraMgr.GameCamController.Target = _currentUnitData.gameObject;
        yield return Timing.WaitForSeconds(0.5f);
        _currentUnitController = _currentUnitData.GetComponent<UnitController>();
        yield return Timing.WaitUntilDone(_currentUnitController._PerformUnitTurn().RunCoroutine());
        yield break;
    }
    public void RemoveUnit(UnitData unit)
    {
        _unitList.Remove(unit);
        _unitQueue.TryRemove(unit);
    }
    private void ResetUnitQueue()
    {
        _unitQueue.Clear();
        foreach(UnitData unitData in _unitList)
        {
            _unitQueue.Enqueue(unitData);
        }
    }
    public void Clear()
    {
        _currentTurn = Define.Turn.Player;
        _unitList.Clear();
        _unitList = null;
    }
}
