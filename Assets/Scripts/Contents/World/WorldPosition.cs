using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPosition : IEquatable<WorldPosition>
{
    public WorldPosition(Define.World world,int level) 
    {
        World = world;
        Level = level;
    }
    public Define.World World;
    public int Level;

    public override int GetHashCode()
    {
        return World.GetHashCode()+Level.GetHashCode();
    }
    public bool Equals(WorldPosition other)
    {
        return (other.World == World && other.Level == Level);
    }
}
