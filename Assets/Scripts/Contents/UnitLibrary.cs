using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class UnitLibrary
{
    //Todo
    public static AbandonedMineShaftDex _abandonedMinshaftDex;
    static UnitLibrary()
    {
        _abandonedMinshaftDex = new AbandonedMineShaftDex();
    }
    static UnitDex GetUnitDex(Define.World world)
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
    public abstract class UnitDex
    {
        public class BaseUnitStat
        {
            public string name;
            public int Speed { get; protected set; } = 5;
            public float MoveSpeed { get; protected set; } = 3.5f;
            
            public int MaxAp { get; protected set; } = 4;
            public int RecoverAp { get; protected set; } = 3;
            public int CurrentAp { get; protected set; } = 3;
            public int EyeSight { get; protected set; } = 10;

            public Define.UnitMentalState Mental { get; protected set; } = Define.UnitMentalState.Hostile;
            public Define.WeaponType Weapon { get; protected set; } = Define.WeaponType.None;
        }

        public abstract BaseUnitStat GetUnit(string unitName);
    }
    public class AbandonedMineShaftDex : UnitDex
    {
        public static Miner Unit_Miner;
        public AbandonedMineShaftDex()
        {
            Unit_Miner = new Miner();
        }
        public class Miner : BaseUnitStat
        {
            public Miner()
            {
                base.name = "Miner";
            }
        }
        public override BaseUnitStat GetUnit(string unitName)
        {
            return (BaseUnitStat)typeof(AbandonedMineShaftDex).GetField(unitName).GetValue(_abandonedMinshaftDex);
        }

    }
}
