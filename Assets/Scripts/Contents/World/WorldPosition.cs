using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPosition
{
    public WorldPosition(Define.World world,int level)
    {
        World = world;
        Level = level;
    }
    public Define.World World;
    public int Level;
}
