using System;
//using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Priority_Queue;
using MEC;
using System.Linq;
using Random = UnityEngine.Random;

public class UnitController : MonoBehaviour
{
    AnimationController _animController;
    UnitData _unitData;
    UnitData.NextActionData _nextAction;
    Define.UnitState _state;
    static Dictionary<Vector3Int, TileInfo> _board;
    Dictionary<Vector3Int, PathInfo> _reachableEmptyTileDict;
    HashSet<Vector3Int> _reachableOccupiedCoorSet;
    Stack<Vector3Int> _path;
    Vector3Int _currentUnitCellPos;

    GameObject _target;
    Vector3Int _destination;
    


    void Init()
    {
        _animController = gameObject.GetComponent<AnimationController>();
        _path = new Stack<Vector3Int>();
        _unitData = transform.GetComponent<UnitData>();
        _board = Managers.DungeonMgr.GetTileInfoDict();
    }
    private void Awake()
    {
        Init();
    }

    public void HandleIdle()
    {
        _animController.PlayAnimation("idle");
        _state = Define.UnitState.Idle;
        ResetPath();
        UpdateReachableTileInfo();
        SetReachableTiles();
        DoNextAction(_unitData.CaculateNextAction());
    }
    public void DoNextAction(UnitData.NextActionData nextAction)
    {
        _nextAction = nextAction;
        switch (nextAction.UnitPurpose)
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

    private void HandlePass()
    {
        _unitData.CurrentState = Define.UnitState.Idle;
        EndTurn();
    }

    private void HandleRoam()
    {
        _unitData.CurrentState = Define.UnitState.Moving;
        UpdateRoamDestination();
        UpdatePath();
        Managers.TurnMgr.HandleUnitTurn();
    }

    private void HandleAttack()
    {
        _unitData.CurrentState = Define.UnitState.Skill;
        _target = _nextAction.BlackTarget; 
        Managers.TurnMgr.HandleUnitTurn();
    }

    private void HandleApproach()
    {
        _unitData.CurrentState = Define.UnitState.Moving;
        _target = _nextAction.BlackTarget == null ? _nextAction.WhiteTarget : _nextAction.BlackTarget;
        UpdateApproachDestination();
        UpdatePath();
        Managers.TurnMgr.HandleUnitTurn();
    }

    private void HandleHelp()
    {
        _unitData.CurrentState = Define.UnitState.Skill;
        _target = _nextAction.WhiteTarget;
        Managers.TurnMgr.HandleUnitTurn();
    }

    private void HandleRunAway()
    {
        //Todo
        //if there are no where to go, turn to mad 
        _unitData.CurrentState = Define.UnitState.Moving;
        UpdateRunAwayDestination();
        UpdatePath();
        Managers.TurnMgr.HandleUnitTurn();
    }

    public IEnumerator<float> HandleMoving()
    {
        ResetReachableTiles();

        #region Unit Moving Algorithm
        yield return Timing.WaitUntilDone(_MoveUnitAlongPath().RunCoroutine());
        Managers.TurnMgr.HandleUnitTurn();
        yield break;
        #endregion
    }

    public void HandleDie()
    {
        throw new NotImplementedException();
    }

    public void HandleUseItem()
    {
        throw new NotImplementedException();
    }

    public void HandleSkill()
    {
        throw new NotImplementedException();
    }
    //Todo duplicate code
    #region Reachable Tile Caculation Algorithm
    public class PathInfo : Interface.ICustomPriorityQueueNode<int>
    {
        Vector3Int _coor;
        Vector3Int _parent;
        int _cost;
        bool _visited;
        public Vector3Int Coor { get { return _coor; } }
        public Vector3Int Parent { get { return _parent; } }
        public int Cost { get { return _cost; } }
        public PathInfo(Vector3Int coor, Vector3Int parent, int cost)
        {
            _coor = coor;
            _parent = parent;
            _cost = cost;
        }
        public int GetPriority()
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
        int currentAp = _unitData.CurrentAp;
        SimplePriorityQueue<PathInfo, int> nextTiles = new SimplePriorityQueue<PathInfo, int>(new PathInfoEquality());

        _reachableEmptyTileDict = new Dictionary<Vector3Int, PathInfo>();
        _reachableOccupiedCoorSet = new HashSet<Vector3Int>();
        PathInfo currentInfo = new PathInfo(_currentUnitCellPos, _currentUnitCellPos, 0);
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
                if (_reachableEmptyTileDict.ContainsKey(nextCoor)) { continue; }
                //nextCoor is not in the dictionary
                int totalMoveCost = currentInfo.Cost + Define.TileMoveCost[i] + _board[currentCoor].LeaveCost; /*To do + reachCost*/
                if (_board.ContainsKey(nextCoor) && currentAp >= totalMoveCost)
                {
                    if (_board[nextCoor].Occupied)
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
    private void UpdatePath()
    {
        //Destination -> in reachabledic -> normal
        PathInfo currentInfo = null;
        ResetPath();
        if (_reachableEmptyTileDict.TryGetValue(_destination,out currentInfo))
        {
            while (currentInfo.Coor != currentInfo.Parent)
            {
                _path.Push(currentInfo.Coor);
                _reachableEmptyTileDict.TryGetValue(currentInfo.Parent, out currentInfo);
            }
            return;
        }
        //Destination -> not in reachabledic -> Astar
        #region Astar
        //Todo use MoveSkill
        //Todo make it simple
        Dictionary<Vector3Int, PathInfo> pathDic = UpdatePathAstar();
        pathDic.TryGetValue(_destination, out currentInfo);
        while(currentInfo.Coor != currentInfo.Parent)
        {
            _path.Push(currentInfo.Coor);
            pathDic.TryGetValue(currentInfo.Parent, out currentInfo);
        }
        #endregion
    }
    //Todo make it Simple
    private Dictionary<Vector3Int, PathInfo> UpdatePathAstar()
    {
        SimplePriorityQueue<PathInfo, int> nextTiles = new SimplePriorityQueue<PathInfo, int>(new PathInfoEquality());
        Dictionary<Vector3Int, PathInfo> emptyTileDictAstar = new Dictionary<Vector3Int, PathInfo>();
        PathInfo currentInfo = new PathInfo(_currentUnitCellPos, _currentUnitCellPos, 0);
        PathInfo nextInfo;
        Vector3Int currentCoor;
        Vector3Int nextCoor;

        nextTiles.Enqueue(currentInfo);
        while (nextTiles.Count > 0)
        {
            currentInfo = nextTiles.Dequeue();
            currentCoor = currentInfo.Coor;
            emptyTileDictAstar.Add(currentInfo.Coor, currentInfo);
            if(currentCoor == _destination) { break; }
            for (int i = 0; i < Define.TileCoor8Dir.Length; i++)
            {
                nextCoor = currentCoor + Define.TileCoor8Dir[i];
                if (emptyTileDictAstar.ContainsKey(nextCoor)) { continue; }
                //nextCoor is not in the dictionary
                int totalMoveCost = currentInfo.Cost + Define.TileMoveCost[i] + _board[currentCoor].LeaveCost
                    + (_destination - nextCoor).sqrMagnitude * 3; /*To do + reachCost*/
                if (_board.ContainsKey(nextCoor))
                {
                    if (_board[nextCoor].Occupied) { continue; }
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
        return emptyTileDictAstar;
    }

    private void UpdateRoamDestination()
    {
        //Roam -> RandomCellPos from ReachableDic;
        _destination = _reachableEmptyTileDict.ElementAt(Random.Range(0, _reachableEmptyTileDict.Count)).Key;
    }
    private void UpdateApproachDestination()
    {
        _destination = _target.GetComponent<BaseUnitData>().CurrentCellCoor;
    }
    private void UpdateRunAwayDestination()
    {
        //Todo
    }

    private void ResetPath()
    {
        _path.Clear();
    }
    private void SetReachableTiles()
    {
        foreach (KeyValuePair<Vector3Int, PathInfo> pair in _reachableEmptyTileDict)
        {
            Managers.UI_Mgr.PaintReachableEmptyTile(pair.Key);
        }
        Timing.WaitForSeconds(0.5f);
    }
    private void ResetReachableTiles()
    {
        foreach (KeyValuePair<Vector3Int, PathInfo> pair in _reachableEmptyTileDict)
        {
            Managers.UI_Mgr.ResetReachableTile(pair.Key);
        }
    }
    private IEnumerator<float> _MoveUnitAlongPath()
    {
        Vector3Int next;
        while (_path.Count > 0)
        {
            next = _path.Pop();
            UpdateUnitLookDir(next);
            _animController.PlayAnimation("walk");

            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_MoveUnitOnce(next)));
            UpdateMoveResult(next);
            yield return Timing.WaitForSeconds(0.15f);
        }
        yield break;
    }
    private IEnumerator<float> _MoveUnitOnce(Vector3Int next)
    {

        Vector3 startingPos = transform.position;
        Vector3 nextDest = Managers.GameMgr.Floor.GetCellCenterWorld(next);
        Vector3 dir = nextDest - startingPos;
        float moveSpeed = Managers.GameMgr.Player_Data.Movespeed;
        while ((transform.position - nextDest).magnitude > 0.1f)
        {
            transform.Translate(dir.normalized * Mathf.Clamp(moveSpeed * Timing.DeltaTime, 0, dir.magnitude));
            yield return 0f;
        }
        yield break;
    }
    public void UpdateUnitLookDir(Vector3Int next)
    {
        Vector3Int dir = next - _currentUnitCellPos;
        if (dir == Define.TileCoor8Dir[4])
        {
            Managers.GameMgr.Player_Data.LookDir = Define.CharDir.Up;

        }
        else if (dir == Define.TileCoor8Dir[6])
        {
            Managers.GameMgr.Player_Data.LookDir = Define.CharDir.Down;
        }
        else if (dir.y >= 0 && dir.x <= 0)
        {
            Managers.GameMgr.Player_Data.LookDir = Define.CharDir.Left;
        }
        else
        {
            Managers.GameMgr.Player_Data.LookDir = Define.CharDir.Right;
        }
    }
    public void UpdateMoveResult(Vector3Int next)
    {
        _currentUnitCellPos = next;
        // calculate current Ap
        // if something happened -> Kill Coroutine
        if (SomethingHappened())
        {
            Timing.KillCoroutines();
            _unitData.CurrentState = Define.UnitState.Idle;
            Managers.TurnMgr.HandleUnitTurn();
        }

    }
    public bool SomethingHappened()
    {
        return false;
    }
    public void EndTurn()
    {
        _unitData.UpdateAp(_unitData.RecoverAp);
        ResetReachableTiles();
        Managers.TurnMgr.UpdateUnit();
    }

}
