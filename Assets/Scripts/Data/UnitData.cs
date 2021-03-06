using MEC;
using Priority_Queue;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitData : BaseUnitData, Interface.ICustomPriorityQueueNode<int>
{
    Define.UnitState _currentState;
    PlayerData _playerData;
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
    public override void Init()
    {
        base.Init();
        _currentState = Define.UnitState.Idle;
        UnitName = "Miner";
        #region Init more state
        _foundPlayer = false;
        _angry = false;
        _lowHealth = false;
        _canAttackPlayer = false;
        #endregion
    }
    public int GetPriority()
    {
        return Stat.Priority;
    }


    public override bool IsDead()
    {
        bool isUnitDead = false;
        return isUnitDead || base.IsDead();
    }
    public override IEnumerator<float> _PerformDeath()
    {
        base._PerformDeath().RunCoroutine();
        Managers.TurnMgr.RemoveUnit(this);
        CurrentState = Define.UnitState.Die;
        GetComponent<UnitController>()._PerformAction().CancelWith(gameObject).RunCoroutine();
        yield break;
    }
    public override void Response()
    {
        base.Response();

    }
    Dictionary<Vector3Int, PathInfo> _reachableEmptyTileDict;
    HashSet<Vector3Int> _reachableOccupiedCoorSet;
    Stack<Vector3Int> _path;
    Vector3Int _destination;
    Vector3Int _memorizedDestination;
    NextActionData _nextActionData;
    UnitController _unitController;

    public class NextActionData
    {
        //public Define.UnitState NextState { get; set; }


        public Define.UnitPurpose UnitPurpose { get; private set; }
        public bool IsNewPurpose { get; set; }
        public List<KeyValuePair<Vector3Int, GameObject>> BlackList { get; set; } = new List<KeyValuePair<Vector3Int, GameObject>>();
        public List<KeyValuePair<Vector3Int, GameObject>> WhiteList { get; set; } = new List<KeyValuePair<Vector3Int, GameObject>>();
        public Dictionary<Vector3Int, GameObject> InSightUnitDict { get; set; } = new Dictionary<Vector3Int, GameObject>();
        public Vector3Int TargetPos;
        public BaseSkill NextSkill { get; set; }
        public void UpdatePurpose(Define.UnitPurpose newPurpose)
        {
            if (UnitPurpose == newPurpose && UnitPurpose != Define.UnitPurpose.PassTurn) { IsNewPurpose = false; } 
            else { IsNewPurpose = true; UnitPurpose = newPurpose; }
        }
    }

 
    void InitNextAction()
    {
        //Todo
        _reachableEmptyTileDict.Clear();
        _reachableOccupiedCoorSet.Clear();
        _path.Clear();
        _nextActionData.InSightUnitDict.Clear();
        _nextActionData.BlackList.Clear();
        _nextActionData.WhiteList.Clear();
    }
    public NextActionData UpdateNextAction()
    {
        _reachableEmptyTileDict = new Dictionary<Vector3Int, PathInfo>();
        _reachableOccupiedCoorSet = new HashSet<Vector3Int>();
        _path = new Stack<Vector3Int>();
        if (_unitController == null) { _unitController = GetComponent<UnitController>(); }
        if(_nextActionData == null) { _nextActionData = new NextActionData(); } 
        else { InitNextAction(); }

        CheckUnitsInSight();
        UpdateReachableTileInfo();
        UpdateUnitMentalState();
        CalculateUnitPurpose();
        HandleUnitPurpose();
        return _nextActionData;
    }
    #region Reachable Tile Caculation Algorithm
    public class PathInfo : Interface.ICustomPriorityQueueNode<int>
    {
        protected Vector3Int _coor;
        protected Vector3Int _parent;
        protected int _cost;
        protected bool _visited;
        public Vector3Int Coor { get { return _coor; } }
        public Vector3Int Parent { get { return _parent; } }
        public int Cost { get { return _cost; } }
        public PathInfo(Vector3Int coor, Vector3Int parent, int cost)
        {
            _coor = coor;
            _parent = parent;
            _cost = cost;
        }
        public virtual int GetPriority()
        {
            return _cost;
        }
    }
    class PathInfoEquality : IEqualityComparer<PathInfo>
    {
        public bool Equals(PathInfo x, PathInfo y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x.Coor == y.Coor;
        }
        public int GetHashCode(PathInfo obj)
        {
            return obj.Coor.GetHashCode();
        }
    }
    private void UpdateReachableTileInfo()
    {
        SimplePriorityQueue<PathInfo, int> nextTiles = new SimplePriorityQueue<PathInfo, int>(new PathInfoEquality());

        _reachableEmptyTileDict = new Dictionary<Vector3Int, PathInfo>();
        _reachableOccupiedCoorSet = new HashSet<Vector3Int>();
        PathInfo currentInfo = new PathInfo(CurrentCellCoor, CurrentCellCoor, 0);
        PathInfo nextInfo;
        Vector3Int currentCoor;
        Vector3Int nextCoor;

        nextTiles.Enqueue(currentInfo);
        while (nextTiles.Count > 0)
        {
            currentInfo = nextTiles.Dequeue();
            currentCoor = currentInfo.Coor;
            _reachableEmptyTileDict.Add(currentInfo.Coor, currentInfo);
            for (int i = 0; i < Define.TileCoor8Dir.Length; i++)
            {
                nextCoor = currentCoor + Define.TileCoor8Dir[i];
                if (_reachableEmptyTileDict.ContainsKey(nextCoor)|| !Managers.GameMgr.IsReachableTile(nextCoor)) { continue; }
                //nextCoor is not in the dictionary
                int totalMoveCost = currentInfo.Cost + Define.TileMoveCost[i] + Managers.GameMgr.GetTile(currentCoor).LeaveCost; /*To do + reachCost*/
                if (Stat.Ap >= totalMoveCost)
                {
                    if (Managers.GameMgr.IsTileOccupied(nextCoor))
                    {
                        _reachableOccupiedCoorSet.Add(nextCoor);
                        continue;
                    }
                    nextInfo = new PathInfo(nextCoor, currentCoor, totalMoveCost);
                    if (nextTiles.TryGetPriority(nextInfo, out int priority))
                    {
                        //nextInfo is in the Queue
                        if (totalMoveCost < priority)
                        {
                            //found better path
                            nextTiles.Remove(nextInfo);
                            nextTiles.Enqueue(nextInfo);
                        }
                        continue;
                    }
                    //nextInfo is not in the Queue, therefore Enqueue nextInfo
                    nextTiles.Enqueue(nextInfo);
                }
            }
        }
    }
    #endregion
    private void UpdateUnitMentalState()
    {
        //Todo
        Mental = Define.UnitMentalState.Hostile;
    }
    private void CheckUnitsInSight()
    {
        Vector3Int nextCoor;
        for (int y = -Stat.EyeSight; y <= Stat.EyeSight; y++)
        {
            int k = Mathf.Abs(y);
            for (int x = k - Stat.EyeSight; x <= Stat.EyeSight - k; x++)
            {
                nextCoor = CurrentCellCoor + new Vector3Int(x, y, 0);
                TileInfo nextTileInfo = Managers.GameMgr.GetTile(nextCoor);
                if (nextTileInfo != null && nextTileInfo?.Unit != null)
                {
                    if(x==0 && y == 0) { continue; }
                    _nextActionData.InSightUnitDict.Add(nextCoor, nextTileInfo.Unit);
                }
            }
        }
    }
    private void CalculateUnitPurpose()
    {
        switch (Mental)
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
    #region HandleUnitPurpos
    private void HandleUnitPurpose()
    {
        if(Stat.Ap <= 0) { _nextActionData.UpdatePurpose(Define.UnitPurpose.PassTurn); }
        switch (_nextActionData.UnitPurpose)
        {
            case Define.UnitPurpose.PassTurn:
                HandlePass();
                break;
            case Define.UnitPurpose.Roam:
                HandleRoam();
                break;
            case Define.UnitPurpose.Attack:
                HandleAttack();
                break;
            case Define.UnitPurpose.Approach:
                HandleApproach();
                break;
            case Define.UnitPurpose.Help:
                HandleHelp();
                break;
            case Define.UnitPurpose.RunAway:
                HandleRunAway();
                break;
            default:
                break;
        }
    }

    #region HandlePass
    private void HandlePass()
    {
        //Todo
        if(_currentState == Define.UnitState.Moving) { _currentState = Define.UnitState.Idle; }
    }
    #endregion
    #region HandleRoam
    private void HandleRoam()
    {
        if (_nextActionData.IsNewPurpose)
        {
            _currentState = Define.UnitState.Moving;
            UpdateRoamDestination();
        }
        UpdateRoamPath();
    }
    private void UpdateRoamDestination()
    {
        //Roam -> RandomCellPos from ReachableDic;
        PathInfo pathInfo;
        do
        {
            pathInfo = _reachableEmptyTileDict.ElementAt(Random.Range(0, _reachableEmptyTileDict.Count)).Value;
        } while (pathInfo.Coor == pathInfo.Parent);
        #region Test
        if (Managers.GameMgr.IsTileOccupied(pathInfo.Coor)) { Debug.LogError("Roam Destination already Occupied"); }
        #endregion
        _destination = pathInfo.Coor;
    }
    #endregion 
    private void HandleAttack()
    {
        _currentState = Define.UnitState.Skill;
    }
    #region HandleApproach
    private void HandleApproach()
    {
        if (_nextActionData.IsNewPurpose)
        {
            _currentState = Define.UnitState.Moving;
        }
        UpdateApproachDestination();
        UpdateApproachPath();
    }
    private void UpdateApproachDestination()
    {
        _destination = _nextActionData.TargetPos;
        if (_reachableOccupiedCoorSet.Contains(_destination)) { GetClosestReachableCoorFromDestination(); }
        //Todo
        else { GetClosestReachableCoorToDestination(); }
    }
    #endregion
    private void HandleHelp()
    {
        _currentState = Define.UnitState.Skill;
    }
    #region HandleRunAway
    private void HandleRunAway()
    {
        //Todo
        //if there are no where to go, turn to mad 
        UpdateRunAwayDestination();
        UpdateRunAwayPath();
        _currentState = Define.UnitState.Moving;
    }


    private void UpdateRunAwayDestination()
    {
        //Todo
    }
    #endregion
    private void GetClosestReachableCoorFromDestination()
    {
        float closest = float.MaxValue;
        float now;
        Vector3Int closestDir = Vector3Int.zero;
        foreach (Vector3Int dir in Define.TileCoor4Dir)
        {
            now = (CurrentCellCoor - _destination - dir).magnitude;
            if(closest > now)
            {
                closest = now;
                closestDir = dir;
            }
        }
        #region Test
        if(closestDir == Vector3Int.zero) { Debug.LogError("new destination is the same"); }
        #endregion
        _destination += closestDir;
    }
    private void GetClosestReachableCoorToDestination()
    {
        float closest = float.MaxValue;
        float now;
        Vector3Int closestDestination = CurrentCellCoor;
        foreach (KeyValuePair<Vector3Int, PathInfo> pair in _reachableEmptyTileDict)
        {
            now = (pair.Key - _destination).magnitude;
            if (closest > now)
            {
                closest = now;
                closestDestination = pair.Key;
            }
        }
        if(closestDestination == CurrentCellCoor) { Debug.Log("new destination is the same"); }
        _destination = closestDestination;
    }
    public Vector3Int? GetNextCoor()
    {
        if(_path.Count == 0) { return null; }
        return _path.Pop();
    }
    public void UpdateMoveResult(Vector3Int next)
    {
        UpdateMoveAp(next);
        CurrentCellCoor = next;
        //Todo extra
    }
    private void UpdateMoveAp(Vector3Int next)
    {
        _reachableEmptyTileDict.TryGetValue(CurrentCellCoor, out PathInfo nowInfo);
        _reachableEmptyTileDict.TryGetValue(next, out PathInfo nextInfo);
        int cost = nextInfo.Cost - nowInfo.Cost;
        UpdateApCost(cost);
    }
    #endregion



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
            foreach (KeyValuePair<Vector3Int, GameObject> blackTarget in _nextActionData.BlackList)
            {
                if (IsTargetInAttackRange(blackTarget))
                {
                    _nextActionData.TargetPos = blackTarget.Key;
                    _nextActionData.UpdatePurpose(Define.UnitPurpose.Attack);
                    return;
                }
            }
            _nextActionData.TargetPos = _nextActionData.BlackList[Random.Range(0, _nextActionData.BlackList.Count)].Key;
            _nextActionData.UpdatePurpose(Define.UnitPurpose.Approach);
            return;
        }
        foreach (KeyValuePair<Vector3Int, GameObject> whiteTarget in _nextActionData.WhiteList)
        {
            if (IsTargetInHelpRange( whiteTarget))
            {
                _nextActionData.TargetPos = whiteTarget.Key;
                _nextActionData.UpdatePurpose(Define.UnitPurpose.Help);
                return;
            }
            _nextActionData.TargetPos = _nextActionData.WhiteList[Random.Range(0, _nextActionData.BlackList.Count)].Key;
            _nextActionData.UpdatePurpose(Define.UnitPurpose.Approach);
            return;
        }
        //Todo
        if (Random.Range(0, 2) == 0) { _nextActionData.UpdatePurpose(Define.UnitPurpose.PassTurn); return; }
        _nextActionData.UpdatePurpose(Define.UnitPurpose.Roam);
    }

    private void UpdateTargetsInSight()
    {
        string nextName;
        foreach(KeyValuePair<Vector3Int,GameObject> pair in _nextActionData.InSightUnitDict)
        {
            nextName = pair.Value.GetComponent<BaseUnitData>().UnitName;
            if (EnemyList.Contains(nextName)) { _nextActionData.BlackList.Add(pair); }
            else if (AllienceList.Contains(nextName)) { _nextActionData.WhiteList.Add(pair); }
        }
    }

    private bool IsTargetInAttackRange(KeyValuePair<Vector3Int, GameObject> blackTarget)
    {
        Vector3Int cellPos = blackTarget.Key;
        int distance = Mathf.Abs(cellPos.x - CurrentCellCoor.x) + Mathf.Abs(cellPos.y - CurrentCellCoor.y);
        foreach (KeyValuePair<string, BaseSkill> pair in _skillDict)
        {
            //Todo
            if (pair.Value.Cost + Stat.Ap < 0) { continue; }
            if (pair.Value.Range < distance) { continue; }
            _nextActionData.NextSkill = pair.Value;
            return true;
        }
        return false;
    }

    private bool IsTargetInHelpRange(KeyValuePair<Vector3Int, GameObject> whiteTarget)
    {
        //Todo
        Vector3Int cellPos = whiteTarget.Key;
        int distance = Mathf.Abs(cellPos.x - CurrentCellCoor.x) + Mathf.Abs(cellPos.y - CurrentCellCoor.y);
        foreach (KeyValuePair<string, BaseSkill> pair in _skillDict)
        {
            //Todo
            if (pair.Value.Cost > Stat.Ap) { continue; }
            if(pair.Value.Range < distance) { continue; }
            _nextActionData.NextSkill = pair.Value;
            return true;
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


    #region Update Path Algorithm
    private void UpdatePath()
    {
        //Destination -> in reachabledic -> normal
        PathInfo currentInfo;
        _path.Clear();
        if (_reachableEmptyTileDict.TryGetValue(_destination, out currentInfo))
        {
            while (currentInfo.Coor != currentInfo.Parent)
            {
                _path.Push(currentInfo.Coor);
                _reachableEmptyTileDict.TryGetValue(currentInfo.Parent, out currentInfo);
            }
        }
        if(_path.Count == 0) 
        { 
            _nextActionData.UpdatePurpose(Define.UnitPurpose.PassTurn); 
            _currentState = Define.UnitState.Idle;
            HandlePass();
        }
    }
    private void UpdateRoamPath()
    {
        UpdatePath();
    }
    private void UpdateApproachPath()
    {
        Debug.Log("Approach!");
        UpdatePath();        
    }
    private void UpdateRunAwayPath()
    {
        //Todo

    }
    
    #endregion
}
