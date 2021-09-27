using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalLake : BaseWorldGenerationData
{
    public CrystalLake()
    {
        _world = Define.World.CrystalLake;
        LoadData("WorldData/CrystalLake");
    }
}
