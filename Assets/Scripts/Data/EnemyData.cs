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
        public Define.UnitPurpose UnitPurpose { get; set; }
        public List<UnitData> Targets { get; set; } = new List<UnitData>();
        public UnitData Target { get; set; }
        public Dictionary<Vector3Int, UnitData> InSightUnitDict { get; set; }
        
    }
    public NextActionData CaculateNextAction()
    {
        NextActionData nextActionData = new NextActionData();
        if (CheckEndTurnPossibility()) { return null; }
        UpdateUnitMentalState();
        CheckUnitsInSight(nextActionData);
        CalculateUnitPurpose(nextActionData);
        return nextActionData;
    }

    private bool CheckEndTurnPossibility()
    {
        return _currentAp == 0;
    }
    private void UpdateUnitMentalState()
    {
    }
    private void CheckUnitsInSight(NextActionData nextActionData)
    {
        Dictionary<Vector3Int,TileInfo> board = Managers.DungeonMgr.GetTileInfoDict();
        Dictionary<Vector3Int,UnitData> unitDic = Managers.GameMgr.WorldUnitDic[Managers.DungeonMgr.CurrentDungeon];
        Vector3Int nextCoor;
        TileInfo nextTileInfo;
        for (int y = -_eyeSight; y <= _eyeSight; y++)
        {
            for (int x = y - _eyeSight; x <= _eyeSight - y; x++)
            {
                nextCoor = _currentCellCoor + new Vector3Int(x, y, 0);
                if (board.TryGetValue(nextCoor,out nextTileInfo) && nextTileInfo.Occupied != Define.OccupiedType.Empty)
                {
                    if(x==0 && y == 0) { continue; }
                    nextActionData.InSightUnitDict.Add(nextCoor, unitDic[nextCoor]);
                }
            }
        }
    }
    private void CalculateUnitPurpose(NextActionData nextActionData)
    {
        switch (_mental)
        {
            case Define.UnitMentalState.Mad:
                HandleMad(nextActionData);
                break;
            case Define.UnitMentalState.Hostile:
                HandleHostile(nextActionData);

                break;
            case Define.UnitMentalState.Neutral:
                HandleNeutral(nextActionData);
                break;
            case Define.UnitMentalState.Friendly:
                HandleFriendly(nextActionData);
                break;
            case Define.UnitMentalState.Reliable:
                HandleReliable(nextActionData);
                break;
            default:
                break;
        }
    }


    private void HandleMad(NextActionData nextActionData)
    {
        throw new System.NotImplementedException();
    }
    #region HandleHostile
    private void HandleHostile(NextActionData nextActionData)
    {
        if (UpdateTargetInSight(nextActionData))
        {
            foreach (UnitData nowTarget in nextActionData.Targets)
            {
                if (IsTarGetInAttackRange())
                {
                    nextActionData.Target = nowTarget;
                    if (IsAllience(nowTarget))
                    {
                        nextActionData.UnitPurpose = Define.UnitPurpose.Help;
                        break;
                    }
                    nextActionData.UnitPurpose = Define.UnitPurpose.Attack;
                    break;
                }
            }
            nextActionData.UnitPurpose = Define.UnitPurpose.Roam;
        }
    }

    private bool UpdateTargetInSight(NextActionData nextActionData)
    {
        throw new System.NotImplementedException();
    }

    private bool IsTarGetInAttackRange()
    {
        throw new System.NotImplementedException();
    }
    private bool IsAllience(UnitData nowTarget)
    {
        throw new System.NotImplementedException();
    }
#endregion

    private void HandleNeutral(NextActionData nextActionData)
    {
        throw new System.NotImplementedException();
    }

    private void HandleFriendly(NextActionData nextActionData)
    {
        throw new System.NotImplementedException();
    }
    private void HandleReliable(NextActionData nextActionData)
    {
        throw new System.NotImplementedException();
    }


}
