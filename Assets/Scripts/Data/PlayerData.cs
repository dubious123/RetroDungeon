using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerData : UnitData
{
    public override void Init()
    {
        base.Init();
    }

    internal int UpdateAp(int cost)
    {
        _currentAp += cost;
        if(_currentAp > _maxAp) { _currentAp = _maxAp; }
        else if(_currentAp < 0) { _currentAp = 0; }
        return _currentAp;
    }
}
