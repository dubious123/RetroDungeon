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
    static Dictionary<Vector3Int, TileInfo> _board;
    


    Vector3Int _destination;
    


    void Init()
    {
        _animController = gameObject.GetComponent<AnimationController>();
        _unitData = transform.GetComponent<UnitData>();
        _board = Managers.DungeonMgr.GetTileInfoDict();
    }
    private void Awake()
    {
        Init();
    }
    public IEnumerator<float> _PerformUnitTurn()
    {
        Debug.Log("unit turn start");   
        do
        {
            _nextAction = _unitData.UpdateNextAction();
            yield return Timing.WaitUntilDone(_PerformAction().RunCoroutine());
        } while (_nextAction.UnitPurpose != Define.UnitPurpose.PassTurn);
        Debug.Log("unit turn end");
        EndTurn();
        yield break;
    }

    public IEnumerator<float> _PerformAction()
    {
        switch (_unitData.CurrentState)
        {
            case Define.UnitState.Idle:
                Debug.Log("Idle");
                HandleIdle();
                yield break;
            case Define.UnitState.Moving:
                Debug.Log("Moving");
                yield return Timing.WaitUntilDone(_HandleMoving().RunCoroutine());
                yield break;
            case Define.UnitState.Skill:
                HandleSkill();
                yield break;
            case Define.UnitState.Die:
                HandleDie();
                yield break;
        }
    }

   


    public void HandleIdle()
    {
        _animController.PlayAnimation("idle");
    }
    public IEnumerator<float> _HandleMoving()
    {

        #region Unit Moving Algorithm
        Vector3Int? nextCoor = _unitData.GetNextCoor();
        if(nextCoor.HasValue) { yield return Timing.WaitUntilDone(_MoveUnitOnce(nextCoor.Value).RunCoroutine()); 
            _unitData.UpdateMoveResult(nextCoor.Value); }
        else { Debug.LogError("nextCoor is null"); }
        yield return Timing.WaitForSeconds(0.5f);
        #endregion
        yield break;
    }

    public void HandleDie()
    {
        throw new NotImplementedException();
    }
    public void HandleSkill()
    {
        throw new NotImplementedException();
    }
    //Todo duplicate code
    private IEnumerator<float> _MoveUnitOnce(Vector3Int next)
    {
        UpdateUnitLookDir(next);
        _animController.PlayAnimation("walk");
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
        Vector3Int dir = next - _unitData.CurrentCellCoor;
        if (dir == Define.TileCoor8Dir[4])
        {
            _unitData.LookDir = Define.CharDir.Up;

        }
        else if (dir == Define.TileCoor8Dir[6])
        {
            _unitData.LookDir = Define.CharDir.Down;
        }
        else if (dir.y >= 0 && dir.x <= 0)
        {
            _unitData.LookDir = Define.CharDir.Left;
        }
        else
        {
            _unitData.LookDir = Define.CharDir.Right;
        }
    }
    private void EndTurn()
    {
        _unitData.UpdateAp(_unitData.RecoverAp);
        Managers.TurnMgr._HandleUnitTurn();
    }
}
