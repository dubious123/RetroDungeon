using System;
//using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Priority_Queue;
using MEC;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Todo playerData
    AnimationController _animController;
    Button _endTernBtn;
    //Define.UnitState _state;
    static Dictionary<Vector3Int, TileInfo> _board;
    Dictionary<Vector3Int, PathInfo> _reachableEmptyTileDict;
    HashSet<Vector3Int> _reachableOccupiedCoorSet; 

    Stack<Vector3Int> _path;
    Vector3Int? _currentMouseCellPos;
    Vector3Int _currentPlayerCellPos;
    Vector3Int _destination;
    void Init()
    {

        _animController = gameObject.GetComponent<AnimationController>();
        Managers.TurnMgr.GetPlayerController(this);
        _path = new Stack<Vector3Int>();
        _currentMouseCellPos = Vector3Int.zero;
        _endTernBtn = GameObject.Find("EndTurnButton").GetComponent<Button>();
        _endTernBtn.onClick.AddListener(EndTurn);
    }
    private void Awake()
    {
        Init();
    }
    public void UpdateMouseScreenPosition(InputAction.CallbackContext context)
    {
        _currentMouseCellPos = Managers.InputMgr.UpdateMouseCellPos(context.ReadValue<Vector2>());
        if (_currentMouseCellPos.HasValue)
        {
            UpdatePath();
        }
    }
    public void OnClicked(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_currentMouseCellPos.HasValue && _reachableEmptyTileDict.ContainsKey(_currentMouseCellPos.Value)) 
            {
                //Todo
                if(_board[_currentMouseCellPos.Value].Occupied)
                {
                    //Todo
                    return;
                }
                Managers.TurnMgr.UpdatePlayerState(Define.UnitState.Moving);
            }
        }
    }
    public void HandleIdle()
    {
        _animController.PlayAnimation("idle");
        //_state = Define.UnitState.Idle;
        //Todo 
        if(_board == null)
        {
            _board = Managers.DungeonMgr.GetTileInfoDict();
        }
        UpdateReachableTileInfo();
        SetReachableTiles();
        Managers.InputMgr.GameInputController.ActivatePlayerInput();

        _endTernBtn.enabled = true;
    }

    public IEnumerator<float> HandleMoving()
    {
        _endTernBtn.enabled = false;
        //_state = Define.UnitState.Moving;
        Managers.InputMgr.GameInputController.DeactivatePlayerInput();

        ResetReachableTiles();
        #region Player Moving Algorithm
        yield return Timing.WaitUntilDone(_MovePlayerAlongPath().RunCoroutine());
        Managers.TurnMgr.UpdatePlayerState(Define.UnitState.Idle);
        yield break;
        #endregion
    }

    public void HandleDie()
    {
        throw new NotImplementedException();
    }
    public void HandleSkill()
    {
        throw new NotImplementedException();
    }


    #region Reachable Tile Caculation Algorithm
    class PathInfo :Interface.ICustomPriorityQueueNode<int>
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
        _reachableOccupiedCoorSet = new HashSet<Vector3Int>();
        PathInfo currentInfo = new PathInfo(_currentPlayerCellPos, _currentPlayerCellPos, 0);
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
        if(_destination == _currentMouseCellPos) { return; }
        if (_reachableEmptyTileDict.TryGetValue(_currentMouseCellPos.Value,out PathInfo currentInfo) == false)
        {
            return;
        }
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
        _destination = _currentMouseCellPos.Value;
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
        foreach(Vector3Int coor in _reachableOccupiedCoorSet)
        {
            Managers.UI_Mgr.PaintReachableOccupiedTile(coor);
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
        Vector3Int dir = next - _currentPlayerCellPos;
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
        // calculate current Ap
        _board[_currentPlayerCellPos].RemoveUnit();
        _board[next].SetUnit(gameObject);
        UpdateMoveAp(next);
        _currentPlayerCellPos = next;

        // if something happened -> Kill Coroutine
        if (SomethingHappened())
        {
            Timing.KillCoroutines();
        }

    }
    public void UpdateMoveAp(Vector3Int next)
    {
        _reachableEmptyTileDict.TryGetValue(_currentPlayerCellPos, out PathInfo nowInfo);
        _reachableEmptyTileDict.TryGetValue(next, out PathInfo nextInfo);
        int cost = nextInfo.Cost - nowInfo.Cost;
        Managers.GameMgr.Player_Data.UpdateAp(-cost);
    }
    public bool SomethingHappened()
    {
        return false;
    }
    public void EndTurn()
    {
        Managers.GameMgr.Player_Data.UpdateAp(Managers.GameMgr.Player_Data.RecoverAp);
        ResetReachableTiles();
        Managers.TurnMgr.UpdateTurn(Define.Turn.Enemy);
    }


}
