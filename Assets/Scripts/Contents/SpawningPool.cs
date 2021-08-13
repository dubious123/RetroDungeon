using Priority_Queue;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawningPool : MonoBehaviour
{
    GameObject _dungeon;
    //public Dictionary<Vector3Int, GameObject> UnitDic;
    private class UnitPriorityComparer : IComparer<int>
    {
        public int Compare(int xSpeed, int ySpeed)
        {
            if(xSpeed > ySpeed) { return ySpeed; }
            else { return xSpeed; }
        }
    }
    public SimplePriorityQueue<UnitData, int> EnemyQueue;
    Dictionary<Vector3Int, TileInfo> _board;
    Tilemap _floor;
    private void Awake()
    {
        _dungeon = transform.gameObject;
        EnemyQueue = new SimplePriorityQueue<UnitData, int>(new UnitPriorityComparer());
    }
    public void SpawnUnits()
    {
        //if (Managers.GameMgr.WorldUnitDic.TryGetValue(_dungeon, out UnitDic) == false)
        //{
        //    UnitDic = new Dictionary<Vector3Int, GameObject>();
        //}
        GameObject newUnit;
        KeyValuePair<Vector3Int, TileInfo> pair;
        DungeonInfo dungeonInfo = _dungeon.GetComponent<DungeonInfo>();
        _board = dungeonInfo.Board;
        _floor = dungeonInfo.tilemaps[0];
        Stack<Vector3Int> randomCoorStack = new Stack<Vector3Int>();
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
            //UnitDic.Add(pair.Key, newUnit);
            newUnit.transform.position = _floor.GetCellCenterWorld(pair.Key);
            EnemyQueue.Enqueue(newUnitData);
        }
    }
}
