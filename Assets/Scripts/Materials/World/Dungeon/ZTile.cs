using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ZTile : MonoBehaviour
{
    Define.World _worldtype = Define.World.Unknown;
    
    public ZTile(Define.World world)
    {
        _worldtype = world;
    }
    public ZTile CreateTile()
    {
        ZTile zTile = new ZTile(_worldtype);
        return zTile;
    }
    public void SetPos(Vector2 cart)
    {
        this.transform.position = cart.ConvertToIso();
    }
}
