using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbandonedMineShaft : BaseWorldGenerationData
{

    public AbandonedMineShaft()
    {

    }
    public override void LoadData()
    {
        _count = 1;
        _world = Define.World.AbandonedMineShaft;
    }
}
