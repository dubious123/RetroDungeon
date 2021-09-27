using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalLake : BaseWorldGenerationData
{
    public CrystalLake()
    {

    }


    public override void LoadData()
    {
        _count = 1;
        _world = Define.World.CrystalLake;
    }
}
