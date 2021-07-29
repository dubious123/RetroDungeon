using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileInfo
{
    Vector3 _position;
    int _level = 0;
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
    public int Level { get { return _level; } }

    public TileInfo(Define.World world, Vector2 cart,ref int preLevel)
    {
        Init(world, cart,ref preLevel);
    }
    public void Init(Define.World world, Vector2 cart, ref int preLevel)
    {
        _worldtype = world;
        SetPos(cart);
        //about 70% -> maintain current level
        int randInt = Random.Range(0,100);
        if( randInt <15 && preLevel > 0) { _level = --preLevel; }
        else if (randInt < 30 && preLevel < 4){ _level = ++preLevel; }
        else { _level = preLevel; }

    }
    void SetPos(Vector2 cart)
    {
        Vector3 iso = cart.ConvertToIso();
        _position.x = iso.x;
        _position.y = iso.y + 0.5f * _level;
        _position.z = _level;
    }


}
