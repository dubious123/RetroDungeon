using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbandonedMineShaft : BaseWorldGenerationData
{

    public AbandonedMineShaft()
    {
        _world = Define.World.AbandonedMineShaft;
        LoadData("WorldData/AbandonedMineShaft");
    }
}
