using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : BaseUnitData, Interface.ICustomPriorityQueueNode<int>
{
    Define.UnitState _currentState;
    PlayerData _playerData;
    int _speed;
    public int Speed { get { return _speed; } set { _speed = value; } }
    public Define.UnitState CurrentState { get { return _currentState; } set { _currentState = value; } }
    //Todo
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

    NextActionData _nextActionData;
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
    public void SetDataFromLibrary(UnitLibrary.UnitDex.BaseUnitStat unitDex)
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
        public List<GameObject> BlackList { get; set; } = new List<GameObject>();
        public GameObject BlackTarget { get; set; }
        public List<GameObject> WhiteList { get; set; } = new List<GameObject>();
        public GameObject WhiteTarget { get; set; }
        public Dictionary<Vector3Int, GameObject> InSightUnitDict { get; set; }
        public SkillLibrary.BaseSkill nextSkill;
    }
    void InitNextAction()
    {
        //Todo
        _nextActionData = new NextActionData();
    }
    public NextActionData CaculateNextAction()
    {
        if(_nextActionData == null) { NextActionData _nextActionData = new NextActionData(); }
        else { InitNextAction(); }
        if (CheckEndTurnPossibility()) { return null; }
        UpdateUnitMentalState();
        CheckUnitsInSight();
        CalculateUnitPurpose();
        return _nextActionData;
    }

    private bool CheckEndTurnPossibility()
    {
        return _currentAp == 0;
    }
    private void UpdateUnitMentalState()
    {
        //Todo
        _mental = Define.UnitMentalState.Hostile;
    }
    private void CheckUnitsInSight()
    {
        Dictionary<Vector3Int,TileInfo> board = Managers.DungeonMgr.GetTileInfoDict();
        Vector3Int nextCoor;
        TileInfo nextTileInfo;
        for (int y = -_eyeSight; y <= _eyeSight; y++)
        {
            for (int x = y - _eyeSight; x <= _eyeSight - y; x++)
            {
                nextCoor = _currentCellCoor + new Vector3Int(x, y, 0);
                if (board.TryGetValue(nextCoor,out nextTileInfo) && nextTileInfo.Unit != null)
                {
                    if(x==0 && y == 0) { continue; }
                    _nextActionData.InSightUnitDict.Add(nextCoor, nextTileInfo.Unit);
                }
            }
        }
    }
    private void CalculateUnitPurpose()
    {
        switch (_mental)
        {
            case Define.UnitMentalState.Mad:
                HandleMad();
                break;
            case Define.UnitMentalState.Hostile:
                HandleHostile();
                break;
            case Define.UnitMentalState.Neutral:
                HandleNeutral();
                break;
            case Define.UnitMentalState.Friendly:
                HandleFriendly();
                break;
            case Define.UnitMentalState.Reliable:
                HandleReliable();
                break;
            default:
                break;
        }
    }


    private void HandleMad()
    {
        throw new System.NotImplementedException();
    }
    #region HandleHostile
    private void HandleHostile()
    {
        UpdateTargetsInSight();
        if (_nextActionData.BlackList.Count > 0)
        {
            foreach (GameObject BlackTarget in _nextActionData.BlackList)
            {
                if (IsTarGetInAttackRange(BlackTarget))
                {
                    _nextActionData.BlackTarget = BlackTarget;
                    _nextActionData.UnitPurpose = Define.UnitPurpose.Attack;
                    return;
                }
            }
            _nextActionData.BlackTarget = _nextActionData.BlackList[Random.Range(0, _nextActionData.BlackList.Count)];
            _nextActionData.UnitPurpose = Define.UnitPurpose.Approach;
            return;
        }
        foreach (GameObject WhiteTarget in _nextActionData.WhiteList)
        {
            if (IsTargetInHelpRange( WhiteTarget))
            {
                _nextActionData.WhiteTarget = WhiteTarget;
                _nextActionData.UnitPurpose = Define.UnitPurpose.Help;
                return;
            }
            _nextActionData.WhiteTarget = _nextActionData.WhiteList[Random.Range(0, _nextActionData.BlackList.Count)];
            _nextActionData.UnitPurpose = Define.UnitPurpose.Approach;
            return;
        }
        //Todo
        if (Random.Range(0, 2) == 0) { _nextActionData.UnitPurpose = Define.UnitPurpose.PassTurn; }
        _nextActionData.UnitPurpose = Define.UnitPurpose.Roam;
    }

    private void UpdateTargetsInSight()
    {
        string nextName;
        foreach(KeyValuePair<Vector3Int,GameObject> pair in _nextActionData.InSightUnitDict)
        {
            nextName = pair.Value.GetComponent<BaseUnitData>().UnitName;
            if (_enemyList.Contains(nextName)) { _nextActionData.BlackList.Add(pair.Value); }
            else if (_allienceList.Contains(nextName)) { _nextActionData.WhiteList.Add(pair.Value); }
        }
    }

    private bool IsTarGetInAttackRange(GameObject blackTarget)
    {
        Vector3Int cellPos = blackTarget.GetComponent<BaseUnitData>().CurrentCellCoor;
        SkillLibrary.BaseSkill nextSkill;
        int distance = Mathf.Abs(cellPos.x - _currentCellCoor.x) + Mathf.Abs(cellPos.y - _currentCellCoor.y);
        foreach(string skill in _skillList)
        {
            //Todo
            nextSkill = SkillLibrary.GetSkill(skill);
            if(nextSkill.Cost > _currentAp) { continue; }
            if (nextSkill.Tags.Contains("Attack")) { _nextActionData.nextSkill = nextSkill; return true; }
        }
        return false;
    }
    private bool IsTargetInHelpRange(GameObject whiteTarget)
    {
        //Todo
        Vector3Int cellPos = whiteTarget.GetComponent<BaseUnitData>().CurrentCellCoor;
        SkillLibrary.BaseSkill nextSkill;
        int distance = Mathf.Abs(cellPos.x - _currentCellCoor.x) + Mathf.Abs(cellPos.y - _currentCellCoor.y);
        foreach (string skill in _skillList)
        {
            //Todo
            nextSkill = SkillLibrary.GetSkill(skill);
            if (nextSkill.Cost > _currentAp) { continue; }
            if (nextSkill.Tags.Contains("Help")) { _nextActionData.nextSkill = nextSkill; return true; }
        }
        return false;
    }
#endregion

    private void HandleNeutral()
    {
        throw new System.NotImplementedException();
    }

    private void HandleFriendly()
    {
        throw new System.NotImplementedException();
    }
    private void HandleReliable()
    {
        throw new System.NotImplementedException();
    }


}
