using System;
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
    static Dictionary<Vector3Int, TileInfo> _board;
    InputAction.CallbackContext _clickContext;
    Vector2 _mouseScreenPos;
    Vector3Int? _clickedCellPos;
    Vector3Int _currentPlayerCellPos;

    
    void Init()
    {      
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _playerInput.DeactivateInput();
        Managers.TurnMgr.SetPlayerController(this);
        transform.position = Managers.GameMgr.Floor.GetCellCenterWorld(Vector3Int.zero);
    }
    private void Awake()
    {
        Init();
    }
    public void UpdateMouseScreenPosition(InputAction.CallbackContext context)
    {
        _mouseScreenPos = context.ReadValue<Vector2>();
    }
    public void OnClicked(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _clickedCellPos = Managers.InputMgr.GetClickedCellPosition(_mouseScreenPos);
            if (_clickedCellPos.HasValue == false) { return; }
            Managers.TurnMgr.UpdatePlayerState(Define.PlayerState.Moving);
        }
    }
    public void HandleIdle()
    {
        //Todo 
        if(_board == null)
        {
            _board = Managers.DungeonMgr.GetTileInfoDict();
        }
        UpdateReachableTileInfo();
        _playerInput.actions.Enable();
    }

    public void HandleMoving()
    {
        _playerInput.actions.Disable();
        
        #region Player Moving Algorithm

        gameObject.transform.position = Managers.GameMgr.Floor.GetCellCenterWorld(_clickedCellPos.Value);
        
        #endregion
        Managers.TurnMgr.UpdatePlayerState(Define.PlayerState.Idle);
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
            return obj.Cost;
        }
    }
    private void UpdateReachableTileInfo()
    {
        int currentAp = Managers.GameMgr.Player_Data.CurrentAp;
        SimplePriorityQueue<PathInfo, int> nextTiles = new SimplePriorityQueue<PathInfo, int>(new PathInfoEquality());
        Dictionary<Vector3Int, PathInfo> reachableTileDict = new Dictionary<Vector3Int, PathInfo>();
        PathInfo currentInfo = new PathInfo(Vector3Int.zero, Vector3Int.zero, 0);
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
                if (_board.ContainsKey(nextCoor) && currentAp < totalMoveCost)
                {
                    nextInfo = new PathInfo(nextCoor, currentCoor, totalMoveCost);
                    if (nextTiles.TryGetPriority(nextInfo,out int priority))
                    {
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

   
   
    
}
