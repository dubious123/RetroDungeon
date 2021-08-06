using System;
//using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Priority_Queue;
using MEC;
public class PlayerController : MonoBehaviour
{
    PlayerInput _playerInput;
    AnimationController _animController;
    Define.PlayerState _state;
    static Dictionary<Vector3Int, TileInfo> _board;
    Dictionary<Vector3Int, PathInfo> _reachableTileDict;
    InputAction.CallbackContext _clickContext;
    Stack<Vector3Int> _path;
    Vector3Int? _currentMouseCellPos;
    Vector3Int _currentPlayerCellPos;
    Vector3Int _destination;
    
    void Init()
    {      
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _playerInput.DeactivateInput();
        _animController = gameObject.GetComponent<AnimationController>();
        Managers.TurnMgr.SetPlayerController(this);
        transform.position = Managers.GameMgr.Floor.GetCellCenterWorld(Vector3Int.zero);

        _path = new Stack<Vector3Int>();
        _currentMouseCellPos = Vector3Int.zero;

        
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
            if (_currentMouseCellPos.HasValue && _reachableTileDict.ContainsKey(_currentMouseCellPos.Value)) 
            {
                Managers.TurnMgr.UpdatePlayerState(Define.PlayerState.Moving);
            }
        }
    }
    public void HandleIdle()
    {
        _animController.PlayAnimation("idle");
        _state = Define.PlayerState.Idle;
        //Todo 
        if(_board == null)
        {
            _board = Managers.DungeonMgr.GetTileInfoDict();
        }
        UpdateReachableTileInfo();
        SetReachableTiles();
        _playerInput.actions.Enable();
    }

    public IEnumerator<float> HandleMoving()
    {
        if(_state == Define.PlayerState.Idle)
        {
            _state = Define.PlayerState.Moving;
            _playerInput.actions.Disable();
            ResetReachableTiles();
        }
        #region Player Moving Algorithm
        yield return Timing.WaitUntilDone(_MovePlayerAlongPath().RunCoroutine());
        Managers.TurnMgr.UpdatePlayerState(Define.PlayerState.Idle);
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

        _reachableTileDict = new Dictionary<Vector3Int, PathInfo>();
        PathInfo currentInfo = new PathInfo(_currentPlayerCellPos, _currentPlayerCellPos, 0);
        PathInfo nextInfo;
        Vector3Int currentCoor;
        Vector3Int nextCoor;

        
        nextTiles.Enqueue(currentInfo);
        while (nextTiles.Count > 0)
        {
            currentInfo = nextTiles.Dequeue();
            currentCoor = currentInfo.Coor;
            _reachableTileDict.Add(currentInfo.Coor, currentInfo);
            for (int i = 0; i < Define.TileCoor8Dir.Length; i++)
            {
                nextCoor = currentCoor + Define.TileCoor8Dir[i];
                if (_reachableTileDict.ContainsKey(nextCoor)) { continue; }
                //nextCoor is not in the dictionary
                int totalMoveCost = currentInfo.Cost + Define.TileMoveCost[i] + _board[currentCoor].LeaveCost; /*To do + reachCost*/
                if (_board.ContainsKey(nextCoor) && currentAp >= totalMoveCost)
                {
                    nextInfo = new PathInfo(nextCoor, currentCoor, totalMoveCost);
                    bool test = nextTiles.Contains(nextInfo);
                    if (nextTiles.TryGetPriority(nextInfo,out int priority))
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
        if(_destination == _currentMouseCellPos) { return; }
        if (_reachableTileDict.TryGetValue(_currentMouseCellPos.Value,out PathInfo currentInfo) == false)
        {
            return;
        }
        UpdateDestination();
        ResetPath();
        while (currentInfo.Coor != currentInfo.Parent)
        {
            _path.Push(currentInfo.Coor);
            _reachableTileDict.TryGetValue(currentInfo.Parent, out currentInfo);
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
        foreach (KeyValuePair<Vector3Int, PathInfo> pair in _reachableTileDict)
        {
            Managers.UI_Mgr.PaintReachableTile(pair.Key);
        }
    }
    private void ResetReachableTiles()
    {
        foreach (KeyValuePair<Vector3Int, PathInfo> pair in _reachableTileDict)
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
            transform.Translate(dir.normalized * Mathf.Clamp(moveSpeed * Time.deltaTime, 0, dir.magnitude));
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
        _currentPlayerCellPos = next;
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
