using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : UnitData, Interface.ICustomPriorityQueueNode<int>
{
    Define.UnitState _currentState;
    PlayerData _playerData;
    int _speed;
    public int Speed { get { return _speed; } set { _speed = value; } }
    public Define.UnitState CurrentState { get { return _currentState; } set { _currentState = value; } }

    #region more state
    bool _foundPlayer;
    bool _angry;
    bool _lowHealth;
    bool _canAttackPlayer;

    bool FoundPlayer { get; set; }
    bool Angry { get; set; }
    bool LowHealth;
    bool CanAttackPlayer;
    #endregion

    public override void Init()
    {
        base.Init();
        _currentState = Define.UnitState.Idle;
        _lookDir = (Define.CharDir)Random.Range(0, 4);

        #region Init more state
        _foundPlayer = false;
        _angry = false;
        _lowHealth = false;
        _canAttackPlayer = false;
        #endregion
    }
    public void SetDataFromLibrary(EnemyLibrary.UnitDex.BaseUnitStat unitDex)
    {
        _speed = unitDex.Speed;
        _moveSpeed = unitDex.MoveSpeed;
        _maxAp = unitDex.MaxAp;
        _recoverAp = unitDex.RecoverAp;
        _weapon = unitDex.Weapon;
    }
    public int GetPriority()
    {
        return _speed;
    }


    public class NextActionData
    {
        public Define.UnitState NextState { get; set; }
    }
    public NextActionData CaculateNextAction()
    {
        if(_playerData == null) { _playerData = Managers.GameMgr.Player_Data; }
        NextActionData nextAction = new NextActionData();
        if((_playerData.CurrentCellCoor - CurrentCellCoor).magnitude < 20.0f){
            _foundPlayer = true; }
        else { _foundPlayer = false; }
        if(_hp/ (float)_maxHp <= 0.1f) { _lowHealth = true; }
        CanAttackPlayer = CaculateCanAttackTarget(_playerData);

        _currentState = nextAction.NextState;   

        return nextAction;
    }

    private bool CaculateCanAttackTarget(UnitData targetData)
    {
        return GetComponent<EnemyController>().ReachableTargetPathDict.ContainsKey(_playerData.CurrentCellCoor);
    }
}
