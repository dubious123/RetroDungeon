using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Dungeon -> Tilemaps -> Tile
/// Dungeon -> DungeonInfo -> Dict(TilePos,TileInfo)
/// TileInfo -> Sprites 
/// </summary>
public class DungeonGenerator
{
    Define.World _world;
    DungeonInfo _dungeonInfo;
    Transform[] TileLayers;
    Vector3 TileCartPos = Vector3.zero;
    TileInfo _tileInfo;
    public DungeonGenerator(Define.World world)
    {
        Init(world);
    }
    void Init(Define.World world)
    {
        _world = world;
    }
    public GameObject GenerateDungeon()
    {

        GameObject dungeon = Managers.Resource.Instantiate("Dungeon");
        if (dungeon == null)
        {
            Debug.Log($"Failed to create Dungeon");
            return null;
        }


        _dungeonInfo = dungeon.GetOrAddComponent<DungeonInfo>();
        _dungeonInfo.Init(_world);


        TileLayers = dungeon.GetChildren();
        ProcedureGenerator();
        return dungeon;
    }
    public TileInfo SetTileInfo()
    {
        //Todo
        TileInfo tileInfo = new TileInfo(_world,TileCartPos);
        tileInfo.sprites[0] = _dungeonInfo.Ground[0];
        return tileInfo;
    }
    public void CreateTile(TileInfo tileInfo)
    {
        for (int i = 0; i < Define.TileLayerNum; i++)
        {
            GameObject tileLayer = Managers.Resource.Instantiate("Tiles/BaseTile", TileLayers[i]);
            tileLayer.GetOrAddComponent<SpriteRenderer>().sprite = tileInfo.sprites[i];
            tileLayer.transform.position = tileInfo.Position;
        }
    }
    void RandomWalk(Vector3 startCartPosition, int DungeonSize)
    {
        TileCartPos = startCartPosition;
        for (int i = 0; i < DungeonSize; i++)
        {
            TileCartPos += Define.TileCart4Dir[Random.Range(0, Define.TileIso4Dir.Length)];
            if (_dungeonInfo.Board.ContainsKey(TileCartPos))
            {
                i--;
                continue;
            }
            _tileInfo = SetTileInfo();
            CreateTile(_tileInfo);
            _dungeonInfo.Board.Add(TileCartPos, _tileInfo);
        }
    }
    void ProcedureGenerator()
    {
        TileCartPos = new Vector3(0, 0, 0);
        for (int i = 0; i < _dungeonInfo._iteration; i++)
        {
            RandomWalk(TileCartPos, 1000);
            KeyValuePair<Vector3, TileInfo> RandomPair =
            _dungeonInfo.Board.ElementAt(Random.Range(0, _dungeonInfo.Board.Count - 1));
            TileCartPos = RandomPair.Key;
        }
    }
}
