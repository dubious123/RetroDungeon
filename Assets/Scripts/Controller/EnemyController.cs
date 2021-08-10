using System;
//using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Priority_Queue;
using MEC;
public class EnemyController : MonoBehaviour
{
    AnimationController _animController;
    EnemyData _enemyData;
    Define.UnitState _state;
    static Dictionary<Vector3Int, TileInfo> _board;
    Dictionary<Vector3Int, PathInfo> _reachableEmptyTileDict;
    HashSet<Vector3Int> _reachableOccupiedCoorSet;
    Dictionary<Vector3Int, PathInfo> _reachableTargetPathDict;
    Stack<Vector3Int> _path;
    Vector3Int _currentUnitCellPos;
    Vector3Int _destination;

    public Dictionary<Vector3Int, PathInfo> ReachableTargetPathDict { get { return _reachableTargetPathDict; } }
    void Init()
    {
        _animController = gameObject.GetComponent<AnimationController>();
        _path = new Stack<Vector3Int>();
        _enemyData = transform.GetComponent<EnemyData>();
    }
    private void Awake()
    {
        Init();
    }

    public void HandleIdle()
    {
        _animController.PlayAnimation("idle");
        _state = Define.UnitState.Idle;
        //Todo 
        if (_board == null)
        {
            _board = Managers.DungeonMgr.GetTileInfoDict();
        }
        UpdateReachableTileInfo();
        SetReachableTiles();
        DoNextAction(_enemyData.CaculateNextAction());
    }
    public void DoNextAction(EnemyData.NextActionData nextAction)
    {
        
        Managers.TurnMgr.HandleEnemyTurn();
    }
    public IEnumerator<float> HandleMoving()
    {
        if (_state == Define.UnitState.Idle)
        {
            _state = Define.UnitState.Moving;
            ResetReachableTiles();
        }
        #region Enemy Moving Algorithm
        yield return Timing.WaitUntilDone(_MovePlayerAlongPath().RunCoroutine());
        Managers.TurnMgr.UpdatePlayerState(Define.UnitState.Idle);
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
        int currentAp = Managers.GameMgr.Player_Data.CurrentAp;
        SimplePriorityQueue<PathInfo, int> nextTiles = new SimplePriorityQueue<PathInfo, int>(new PathInfoEquality());

        _reachableEmptyTileDict = new Dictionary<Vector3Int, PathInfo>();
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
                    nextInfo = new PathInfo(nextCoor, currentCoor, totalMoveCost);
                    bool test = nextTiles.Contains(nextInfo);
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
    private void UpdatePath()
    {
        PathInfo currentInfo = null;
        UpdateDestination();
        ResetPath();
        while (currentInfo.Coor != currentInfo.Parent)
        {
            _path.Push(currentInfo.Coor);
            _reachableEmptyTileDict.TryGetValue(currentInfo.Parent, out currentInfo);
        }
    }

    private void UpdateDestination()
    {
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
    }
    private void ResetReachableTiles()
    {
        foreach (KeyValuePair<Vector3Int, PathInfo> pair in _reachableEmptyTileDict)
        {
            Managers.UI_Mgr.ResetReachableTile(pair.Key);
        }
    }
    private IEnumerator<float> _MovePlayerAlongPath()
    {
        Vector3Int next;
        while (_path.Count > 0)
        {
            next = _path.Pop();
            UpdatePlayerLookDir(next);
            _animController.PlayAnimation("walk");

            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_MovePlayerOnce(next)));
            UpdateMoveResult(next);
            yield return Timing.WaitForSeconds(0.15f);
        }
        yield break;
    }
    private IEnumerator<float> _MovePlayerOnce(Vector3Int next)
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
    public void UpdatePlayerLookDir(Vector3Int next)
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
        }

    }
    public bool SomethingHappened()
    {
        return false;
    }
}
