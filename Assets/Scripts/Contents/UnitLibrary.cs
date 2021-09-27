using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class UnitLibrary
{
    static BaseUnitStat GetUnitBaseStat(string name)
    {
        return new BaseUnitStat();
    }
    public static void SetUnitData(string name,BaseUnitData unitData)
    {
        unitData.Stat = GetUnitBaseStat(name);
    }
}
