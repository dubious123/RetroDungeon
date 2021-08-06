using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Priority_Queue;
public class PlayerController : MonoBehaviour
{
    PlayerInput _playerInput;
<<<<<<< HEAD
=======
    Define.PlayerState _state;
>>>>>>> parent of f02edd4 (08.06.2021)
    static Dictionary<Vector3Int, TileInfo> _board;
    InputAction.CallbackContext _clickContext;
    Vector2 _mouseScreenPos;
    Vector3Int? _clickedCellPos;
    Vector3Int _currentPlayerCellPos;
<<<<<<< HEAD
=======
    Vector3Int _destination;
>>>>>>> parent of f02edd4 (08.06.2021)

    
    void Init()
    {      
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _playerInput.DeactivateInput();
        Managers.TurnMgr.SetPlayerController(this);
        transform.position = Managers.GameMgr.Floor.GetCellCenterWorld(Vector3Int.zero);
<<<<<<< HEAD
=======

        _path = new Stack<Vector3Int>();
        _currentMouseCellPos = Vector3Int.zero;
        
>>>>>>> parent of f02edd4 (08.06.2021)
    }
    private void Awake()
    {
        Init();
    }
    public void FixedUpdate()
    {
    }
    public void UpdateMouseScreenPosition(InputAction.CallbackContext context)
    {
        _mouseScreenPos = context.ReadValue<Vector2>();
    }
    public void OnClicked(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
<<<<<<< HEAD
            _clickedCellPos = Managers.InputMgr.GetClickedCellPosition(_mouseScreenPos);
            if (_clickedCellPos.HasValue == false) { return; }
=======
            if (_currentMouseCellPos.HasValue == false && _reachableTileDict.ContainsKey(_currentMouseCellPos.Value)) { return; }
>>>>>>> parent of f02edd4 (08.06.2021)
            Managers.TurnMgr.UpdatePlayerState(Define.PlayerState.Moving);
        }
    }
    public void HandleIdle()
    {
<<<<<<< HEAD
=======
        //_state = Define.PlayerState.Idle;
>>>>>>> parent of f02edd4 (08.06.2021)
        //Todo 
        if(_board == null)
        {
            _board = Managers.DungeonMgr.GetTileInfoDict();
        }
        _currentPlayerCellPos = _currentMouseCellPos.Value;
        UpdateReachableTileInfo();
<<<<<<< HEAD
=======
        UpdatePath();
        SetReachableTiles();
>>>>>>> parent of f02edd4 (08.06.2021)
        _playerInput.actions.Enable();
    }

    public void HandleMoving()
    {
<<<<<<< HEAD
        _playerInput.actions.Disable();
        
        #region Player Moving Algorithm

        gameObject.transform.position = Managers.GameMgr.Floor.GetCellCenterWorld(_clickedCellPos.Value);
        _currentPlayerCellPos = _clickedCellPos.Value;
        #endregion
        Managers.TurnMgr.UpdatePlayerState(Define.PlayerState.Idle);
=======
        
        _state = Define.PlayerState.Moving;
        _playerInput.actions.Disable();
        ResetReachableTiles();
        #region Player Moving Algorithm
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(MovePlayerAlongPath()));
        //gameObject.transform.position = Managers.GameMgr.Floor.GetCellCenterWorld(_currentMouseCellPos.Value);

        
        #endregion
        if ((transform.position - Managers.GameMgr.Floor.GetCellCenterWorld(_destination)).magnitude<0.01f)
        {
            Managers.TurnMgr.UpdatePlayerState(Define.PlayerState.Idle);
        }
        Managers.TurnMgr.UpdatePlayerState(Define.PlayerState.Moving);
        yield break;
>>>>>>> parent of f02edd4 (08.06.2021)
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

    class PathInfo : Interface.ICustomPriorityQueueNode<int>
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
            return x.Coor == y.Coor;
        }

        public int GetHashCode(PathInfo obj)
        {
            return -obj.Cost;
        }
    }
    private void UpdateReachableTileInfo()
    {
        int currentAp = Managers.GameMgr.Player_Data.CurrentAp;
        SimplePriorityQueue<PathInfo, int> nextTiles = new SimplePriorityQueue<PathInfo, int>(new PathInfoEquality());
        Dictionary<Vector3Int, PathInfo> reachableTileDict = new Dictionary<Vector3Int, PathInfo>();
        PathInfo currentInfo = new PathInfo(_currentPlayerCellPos, _currentPlayerCellPos, 0);
        PathInfo nextInfo;
        Vector3Int currentCoor;
        Vector3Int nextCoor;

        
        nextTiles.Enqueue(currentInfo);
        while (nextTiles.Count > 0)
        {
            currentInfo = nextTiles.Dequeue();
            currentCoor = currentInfo.Coor;
            reachableTileDict.Add(currentInfo.Coor, currentInfo);
            for (int i = 0; i < Define.TileCoor8Dir.Length; i++)
            {
                nextCoor = currentCoor + Define.TileCoor8Dir[i];
                if (reachableTileDict.ContainsKey(nextCoor)) { continue; }
                //nextCoor is not in the dictionary
                int totalMoveCost = currentInfo.Cost + Define.TileMoveCost[i] + _board[currentCoor].LeaveCost; /*To do + reachCost*/
                if (_board.ContainsKey(nextCoor) && currentAp > totalMoveCost)
                {
                    nextInfo = new PathInfo(nextCoor, currentCoor, totalMoveCost);
                    if (nextTiles.TryGetPriority(nextInfo,out int priority))
                    {
                        throw new Exception();
                        //nextInfo is in the Queue
                        if (totalMoveCost < priority) 
                        {
                            //found better path
                            nextTiles.UpdatePriority(nextInfo, totalMoveCost);
                        }
                        continue;
                    }
                    //nextInfo is not in the Queue, therefore Enqueue nextInfo
                    nextTiles.Enqueue(nextInfo);
                }
            }
        }
    }
<<<<<<< HEAD
=======
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
    private IEnumerator<float> MovePlayerAlongPath()
    {
        Vector3Int next;
        while (_path.Count > 0)
        {
            next = _path.Pop();
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(MovePlayerOnce(next)));
            _currentPlayerCellPos = next;
        }
        yield break;
    }
    private IEnumerator<float> MovePlayerOnce(Vector3Int next)
    {
        Vector3 startingPos = transform.position;
        Vector3 nextDest = Managers.GameMgr.Floor.GetCellCenterWorld(next);
        float moveSpeed = Managers.GameMgr.Player_Data.Movespeed;
        while ((transform.position - nextDest).magnitude > 0.01f)
        {
            transform.Translate(Vector3.Lerp(startingPos, nextDest, moveSpeed * Time.deltaTime));
            yield return 0f;
        }
        yield break;
    }
>>>>>>> parent of f02edd4 (08.06.2021)

   
   
    
}
