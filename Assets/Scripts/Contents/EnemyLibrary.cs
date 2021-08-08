using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EnemyLibrary
{
    public static AbandonedMineShaftDex _abandonedMinshaftDex;
    public EnemyLibrary()
    {
        _abandonedMinshaftDex = new AbandonedMineShaftDex();
    }
    public UnitDex GetEnemyDex(Define.World world)
    {
        switch (world)
        {
            case Define.World.Unknown:
                return null;
            case Define.World.AbandonedMineShaft:
                return _abandonedMinshaftDex;
            default:
                return null;
        }
    }
    public class UnitDex
    {
        public class BaseUnitStat
        {
            public int Speed { get; protected set; } = 5;
            public float MoveSpeed { get; protected set; } = 3.5f;

            public int MaxAp { get; protected set; } = 4;
            public int RecoverAp { get; protected set; } = 3;
            public int CurrentAp { get; protected set; } = 3;

            public Define.WeaponType Weapon { get; protected set; } = Define.WeaponType.None;

        }

        public virtual void GetUnit(string unitName)
        {
        }
    }
    public class AbandonedMineShaftDex : UnitDex
    {
        Miner Unit_Miner;
        public AbandonedMineShaftDex()
        {
            Unit_Miner = new Miner();
        }
        public class Miner : BaseUnitStat
        {
        }
        //public override UnitDex GetUnit(string unitName, System.Object obj)
        //{
        //    Type type = obj.GetType();
        //    type.GetProperty(unitName).;
        //    return Unit_Info.GetValue(unit);
        //}

    }
}
