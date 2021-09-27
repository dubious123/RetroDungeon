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
    PlayerData _playerData;
    Define.UnitState _nextState;
    AnimationController _animController;
    Button _endTernBtn;
    Dictionary<Vector3Int, PathInfo> _reachableEmptyTileDict;
    HashSet<Vector3Int> _reachableOccupiedCoorSet;
    Stack<Vector3Int> _path;
    Vector3Int? _currentMouseCellPos;
    Vector3Int _destination;
    Dictionary<Vector3Int, TileInfo> _inRangeTileDict;
    SkillLibrary.BaseSkill _skill;
    Vector3Int _skillTargetPos;

    public Vector3Int? CurrentMouseCellPos { set { _currentMouseCellPos = value; } }
    public Dictionary<Vector3Int, PathInfo> ReachableEmptyTileDict { get { return _reachableEmptyTileDict; } }
    public Dictionary<Vector3Int, TileInfo> InRangeTileDict { get { return _inRangeTileDict; } }
    public HashSet<Vector3Int> ReachableOccupiedCoorSet { get { return _reachableOccupiedCoorSet; } }
    void Init()
    {
        _playerData = GetComponent<PlayerData>();
        _animController = gameObject.GetComponent<AnimationController>();
        Managers.TurnMgr.GetPlayerController(this);
        _path = new Stack<Vector3Int>();
        _inRangeTileDict = new Dictionary<Vector3Int, TileInfo>();
        _currentMouseCellPos = Vector3Int.zero;
        _endTernBtn = GameObject.Find("EndTurnButton").GetComponent<Button>();
        _endTernBtn.onClick.AddListener(EndTurn);
    }
    private void Awake()
    {
        Init();
    }
    public void UpdatePlayerNextState(Define.UnitState unitState) { _nextState = unitState; }
    public void UpdatePlayerState(Define.UnitState nextState)
    {
        Managers.TurnMgr.HandlePlayerTurn(nextState);
    }
    public void HandleIdle()
    {
        _animController.PlayAnimationLoop("idle");
        UpdateReachableTileInfo();
        Managers.UI_Mgr.HideAllOverlay();
        Managers.UI_Mgr.AddTileSet(Define.TileOverlay.Unit, _playerData.CurrentCellCoor,_playerData.TileColor);
        Managers.UI_Mgr.UpdateTileSet(Define.TileOverlay.Move, _reachableEmptyTileDict.Keys);
        Managers.UI_Mgr.ShowOverlay(Define.TileOverlay.Move,Define.TileOverlay.Unit);
        Managers.InputMgr.GameController.ActivatePlayerInput();
        _endTernBtn.enabled = true;
    }
    #region HandleMoving
    public IEnumerator<float> HandleMoving()
    {
        _endTernBtn.enabled = false;
        Managers.InputMgr.GameController.DeactivatePlayerInput();
        Managers.UI_Mgr.HideAllOverlay();
        #region Player Moving Algorithm
        yield return Timing.WaitUntilDone(_MovePlayerAlongPath().RunCoroutine());
        UpdatePlayerState(Define.UnitState.Idle);
        yield break;
        #endregion
    }
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
        int currentAp = Managers.GameMgr.Player_Data.Stat.Ap;
        SimplePriorityQueue<PathInfo, int> nextTiles = new SimplePriorityQueue<PathInfo, int>(new PathInfoEquality());

        _reachableEmptyTileDict = new Dictionary<Vector3Int, PathInfo>();
        _reachableOccupiedCoorSet = new HashSet<Vector3Int>();
        PathInfo currentInfo = new PathInfo(_playerData.CurrentCellCoor, _playerData.CurrentCellCoor, 0);
        PathInfo nextInfo;
        Vector3Int currentCoor;
        Vector3Int nextCoor;

        nextTiles.Enqueue(currentInfo);
        _reachableOccupiedCoorSet.Add(_playerData.CurrentCellCoor);
        Dungeon dungeon = Managers.GameMgr.CurrentDungeon;
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
                int totalMoveCost = currentInfo.Cost + Define.TileMoveCost[i] + dungeon.GetTile(currentCoor).LeaveCost; /*To do + reachCost*/
                if (Managers.GameMgr.HasTile(nextCoor)&& currentAp >= totalMoveCost)
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
    public void UpdatePath(Vector3Int mouseCellPos)
    {
        _currentMouseCellPos = mouseCellPos;
        if (_destination == _currentMouseCellPos) { return; }
        if (_reachableEmptyTileDict.TryGetValue(_currentMouseCellPos.Value, out PathInfo currentInfo) == false)
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
    private IEnumerator<float> _MovePlayerAlongPath()
    {
        Vector3Int next;
        while (_path.Count > 0)
        {
            next = _path.Pop();
            UpdatePlayerLookDir(next);
            _animController.PlayAnimationLoop("run");

            yield return Timing.WaitUntilDone(_MovePlayerOnce(next).RunCoroutine());
            UpdateMoveResult(next);
            yield return Timing.WaitForSeconds(0.1f);
        }
        yield break;
    }
    private IEnumerator<float> _MovePlayerOnce(Vector3Int next)
    {
        Vector3 startingPos = transform.position;
        Vector3 nextDest = Managers.GameMgr.Floor.GetCellCenterWorld(next);
        float moveSpeed = Managers.GameMgr.Player_Data.Stat.MoveSpeed;
        float delta = 0;
        float ratio = 0;
        while (ratio <= 1.0f)
        {
            delta += Timing.DeltaTime;
            ratio = delta * moveSpeed;
            transform.position = Vector3.Lerp(startingPos, nextDest, ratio);
            yield return 0f;
        }
        yield break;
    }
    public void UpdatePlayerLookDir(Vector3Int next)
    {
        Vector3Int dir = next - _playerData.CurrentCellCoor;
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
        UpdateMoveAp(next);
        _playerData.CurrentCellCoor = next;

        // if something happened -> Kill Coroutine
        if (SomethingHappened())
        {
            Timing.KillCoroutines();
        }

    }
    public void UpdateMoveAp(Vector3Int next)
    {
        _reachableEmptyTileDict.TryGetValue(_playerData.CurrentCellCoor, out PathInfo nowInfo);
        _reachableEmptyTileDict.TryGetValue(next, out PathInfo nextInfo);
        int cost = nextInfo.Cost - nowInfo.Cost;
        Managers.GameMgr.Player_Data.UpdateApCost(cost);
    }

    #endregion
    public IEnumerator<float> _HandleDie()
    {
        Managers.InputMgr.GameController.DeactivatePlayerInput();
        Managers.InputMgr.GameController.DeactivateCameraScroll();
        yield return Timing.WaitUntilDone(_animController._PlayAnimation("vanish",1).RunCoroutine());
        Managers.ResourceMgr.Destroy(gameObject);
        Managers.GameMgr.PerformPlayerLose();
        yield break;


    }
    #region HandleSkill
    public IEnumerator<float> HandleSkill()
    {
        _endTernBtn.enabled = false;
        Managers.InputMgr.GameController.DeactivatePlayerInput();
        Managers.UI_Mgr.HideOverlay(Define.TileOverlay.Skill);
        Managers.BattleMgr.SkillFromTo(_playerData, _skillTargetPos, _skill);
        yield return Timing.WaitUntilDone(_animController._PlayAnimation($"{_skill.AnimName}", 1).RunCoroutine());
        ResetSkill();
        UpdatePlayerState(Define.UnitState.Idle);
        yield break;
    }
    public void UpdateTargetPos(Vector3Int pos)
    {
        _skillTargetPos = pos;
    }
    public void UpdateSkill(string skillName)
    {
        if (!_playerData.SkillDict.TryGetValue(skillName, out _skill)) { 
            Debug.LogError("Not Learned yet"); return; }
        Managers.UI_Mgr.HideAllOverlay();
        SetInRangeTilesDict();
        Managers.UI_Mgr.UpdateTileSet(Define.TileOverlay.Skill, _inRangeTileDict.Keys);
        Managers.UI_Mgr.ShowOverlay(Define.TileOverlay.Skill,Define.TileOverlay.Unit);
    }
    public void ResetSkill()
    {
        _skill = null;
        Managers.UI_Mgr.HideAllOverlay();
        _inRangeTileDict.Clear();
        Managers.UI_Mgr.ShowOverlay(Define.TileOverlay.Move);
    }
    public void SetInRangeTilesDict()
    {
        int range = _skill.Range;
        Vector3Int nowPos;
        for (int y = -range; y <= range; y++)
        {
            int k = Mathf.Abs(y);
            for (int x = k - range; x <= range - k; x++)
            {
                nowPos = _playerData.CurrentCellCoor + new Vector3Int(x, y, 0);
                if (Managers.GameMgr.HasTile(nowPos)) { _inRangeTileDict.Add(nowPos, Managers.GameMgr.CurrentDungeon.GetTile(nowPos)); }
            }
        }
        if (!_skill.IsSelfIncluded) { _inRangeTileDict.Remove(_playerData.CurrentCellCoor); }
    }
    #endregion


    public bool SomethingHappened()
    {
        return false;
    }
    public void EndTurn()
    {
        Managers.GameMgr.Player_Data.UpdateApRecover(Managers.GameMgr.Player_Data.Stat.RecoverAp);
        Managers.UI_Mgr.HideOverlay(Define.TileOverlay.Move,Define.TileOverlay.Skill);
        _endTernBtn.enabled = false;
        Managers.InputMgr.GameController.DeactivatePlayerInput();
        Managers.TurnMgr._HandleUnitTurn().RunCoroutine();
    }


}
