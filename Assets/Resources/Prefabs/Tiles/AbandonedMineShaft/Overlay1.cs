using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="My Assets/Overlay1")]
public class Overlay1 : RuleTile<Overlay1.Neighbor> 
{

    public class Neighbor : RuleTile.TilingRule.Neighbor 
    {
        public const int Null = 3;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) 
    {
        switch (neighbor) 
        {
            case Neighbor.This: return Check_This(tile);
            case Neighbor.NotThis: return Check_NotThis(tile);
            case Neighbor.Null: return Check_Null(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }
    private bool Check_This(TileBase tile)
    {
        return tile == this;
    }
    private bool Check_NotThis(TileBase tile)
    {
        return tile != this;
    }
    private bool Check_Null(TileBase tile)
    {
        return tile == null; 
    }
}