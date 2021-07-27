using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    public Define.World _currentWorld { get; private set; } = Define.World.Unknown;
    public void Init()
    {
        _currentWorld = Define.World.AbandonedMineShaft;
    }
}
