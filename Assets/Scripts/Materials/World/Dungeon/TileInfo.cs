using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileInfo
{
    Vector3 _position;
    Define.World _worldtype = Define.World.Unknown;
    /// <summary>
    /// 4: AbovePlayerWall
    /// 3:DynamicWall
    /// 2:Pit
    /// 1:FloorOverlay
    /// 0:Floor
    /// </summary>
    public Sprite[] sprites = new Sprite[Define.TileLayerNum];

    public Vector3 Position { get { return _position; } }

    public TileInfo(Define.World world, Vector2 cart)
    {
        Init(world, cart);
    }
    public void Init(Define.World world, Vector2 cart)
    {
        _worldtype = world;
        SetPos(cart);
    }
    void SetPos(Vector2 cart)
    {
        Vector3 iso = cart.ConvertToIso();
        _position.x = iso.x;
        _position.y = iso.y;
        _position.z = 0;
    }


}
