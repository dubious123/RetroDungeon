using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteData
{
    public class AbandonedMineShaft
    {
        public static Sprite[] Ground = { 
        Managers.Resource.GetSprite($"AbandonedMineShaft/Ground/Default"),
        Managers.Resource.GetSprite($"AbandonedMineShaft/Ground/Dirt+Stone_1"),
        Managers.Resource.GetSprite($"AbandonedMineShaft/Ground/Dirt+Stone_1"),
        Managers.Resource.GetSprite($"AbandonedMineShaft/Ground/Dirt_1"),
        Managers.Resource.GetSprite($"AbandonedMineShaft/Ground/Dirt_2"),
        Managers.Resource.GetSprite($"AbandonedMineShaft/Ground/Dirt_3"),
        };
    }
}
