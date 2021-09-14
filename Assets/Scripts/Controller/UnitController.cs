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
        while(true)
        {
            _nextAction = _unitData.UpdateNextAction();
            if(_nextAction.UnitPurpose == Define.UnitPurpose.PassTurn) { break; }
            yield return Timing.WaitUntilDone(_PerformAction().RunCoroutine());
        }
        EndTurn();
        yield break;
    }

    public IEnumerator<float> _PerformAction()
    {
        switch (_unitData.CurrentState)
        {
            case Define.UnitState.Idle:
                HandleIdle();
                yield break;
            case Define.UnitState.Moving:
                yield return Timing.WaitUntilDone(_HandleMoving().RunCoroutine());
                yield break;
            case Define.UnitState.Skill:
                yield return Timing.WaitUntilDone(_HandleSkill().RunCoroutine());
                yield break;
            case Define.UnitState.Die:
                HandleDie();
                yield break;
        }
    }

   


    public void HandleIdle()
    {
        _animController.PlayAnimationLoop("idle");
    }
    public IEnumerator<float> _HandleMoving()
    {

        #region Unit Moving Algorithm
        Vector3Int? nextCoor = _unitData.GetNextCoor();
        if(nextCoor.HasValue) { yield return Timing.WaitUntilDone(_MoveUnitOnce(nextCoor.Value).RunCoroutine()); 
            _unitData.UpdateMoveResult(nextCoor.Value); }
        else { Debug.LogError("nextCoor is null"); }
        yield return Timing.WaitForSeconds(0.1f);
        #endregion
        yield break;
    }

    public void HandleDie()
    {
        throw new NotImplementedException();
    }
    public IEnumerator<float> _HandleSkill()
    {
        Managers.BattleMgr.SkillFromTo(_unitData, _nextAction.TargetPos, _nextAction.NextSkill);
        yield return Timing.WaitUntilDone(_animController._PlayAnimation(_nextAction.NextSkill.AnimName, 1).RunCoroutine());
        //yield return Timing.WaitForSeconds(0.5f);
        //Todo
        yield break;
    }
    //Todo duplicate code
    private IEnumerator<float> _MoveUnitOnce(Vector3Int next)
    {
        UpdateUnitLookDir(next);
        _animController.PlayAnimationLoop("run");
        Vector3 startingPos = transform.position;
        Vector3 nextDest = Managers.GameMgr.Floor.GetCellCenterWorld(next);
        float moveSpeed = Managers.GameMgr.Player_Data.Movespeed;
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
        _unitData.UpdateApRecover(_unitData.RecoverAp);
        //Todo
        _animController.PlayAnimationLoop("idle");
        Managers.TurnMgr._HandleUnitTurn().RunCoroutine();
    }
}
