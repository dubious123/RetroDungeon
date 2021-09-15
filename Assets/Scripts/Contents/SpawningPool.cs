using Priority_Queue;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawningPool : MonoBehaviour
{ 
    GameObject _dungeon;
    public List<UnitData> UnitList;
    Dictionary<Vector3Int, TileInfo> _board;
    Tilemap _floor;
    private void Awake()
    {
        _dungeon = transform.gameObject;
        UnitList = new List<UnitData>();
    }
    public void SpawnUnits()
    {
        GameObject newUnit;
        KeyValuePair<Vector3Int, TileInfo> pair;
        DungeonInfo dungeonInfo = _dungeon.GetComponent<DungeonInfo>();
        _board = dungeonInfo.Board;
        _floor = dungeonInfo.Tilemaps[0];
        for (int i = 0; i < dungeonInfo.EnemyCount; i++)
        {
            //Todo
            newUnit = Managers.ResourceMgr.Instantiate("Unit/BaseUnit", _floor.transform);
            UnitData newUnitData = newUnit.GetComponent<UnitData>();
            newUnitData.SetDataFromLibrary(UnitLibrary._abandonedMinshaftDex.GetUnit("Unit_Miner"));
            while(true)
            {
                pair = _board.ElementAt(Random.Range(0, _board.Count - 1));
                if(!pair.Value.Occupied) 
                {
                    pair.Value.SetUnit(newUnit);
                    break; 
                }
            }
            newUnitData.CurrentCellCoor = pair.Key;
            newUnit.transform.position = _floor.GetCellCenterWorld(pair.Key);
            UnitList.Add(newUnitData);
        }
    }
}
