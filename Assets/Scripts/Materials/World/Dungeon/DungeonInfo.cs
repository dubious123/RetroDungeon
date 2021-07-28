using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonInfo : MonoBehaviour
{
    Define.World _world;
    public Dictionary<Vector3, TileInfo> Board = new Dictionary<Vector3, TileInfo>();
    public int _iteration { get; private set; }
    #region SpriteSets
    public Sprite[] Ground;
    #endregion


    int _rowSize, _columnSize;
    public void Init(Define.World world)
    {
        _world = world;
        SetDefaultSprite();
    }
    void SetDefaultSprite()
    {
        switch (_world)
        {
            case Define.World.Unknown:
                break;
            case Define.World.AbandonedMineShaft:
                Ground = SpriteData.AbandonedMineShaft.Ground;
                //Todo
                _iteration = Random.Range(1, 10);
                break;
            default:
                break;
        }
    }
}
