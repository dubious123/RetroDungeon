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
    public void SetDataFromLibrary(EnemyLibrary.UnitDex.BaseUnitStat unitDex)
    {
        _speed = unitDex.Speed;
        _moveSpeed = unitDex.MoveSpeed;
        _maxAp = unitDex.MaxAp;
        _recoverAp = unitDex.RecoverAp;
        _weapon = unitDex.Weapon;
    }
    public int GetPriority()
    {
        return _speed;
    }

}
