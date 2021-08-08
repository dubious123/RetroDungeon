using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : UnitData, Interface.ICustomPriorityQueueNode<int>
{
    int _speed;
    int Speed { get { return _speed; } set { _speed = value; } }


    public override void Init()
    {
        base.Init();
        _lookDir = (Define.CharDir)Random.Range(0, 4);
    }
    public void SetDataFromLibrary(EnemyLibrary.UnitDex.BaseUnitStat enemyStat)
    {
        _speed = enemyStat.Speed;
        _moveSpeed = enemyStat.MoveSpeed;
        _maxAp = enemyStat.MaxAp;
        _recoverAp = enemyStat.RecoverAp;
        _weapon = enemyStat.Weapon;
    }
    public int GetPriority()
    {
        return _speed;
    }

}
