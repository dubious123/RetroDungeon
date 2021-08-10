using Priority_Queue;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawningPool : MonoBehaviour
{
    GameObject _dungeon;
    public Dictionary<Vector3Int, EnemyData> EnemyDic;
    private class EnemyPriorityComparer : IComparer<int>
    {
        public int Compare(int xSpeed, int ySpeed)
        {
            return -Compare(xSpeed, ySpeed);
        }
    }
    public SimplePriorityQueue<EnemyData, int> EnemyQueue;
    Dictionary<Vector3Int, TileInfo> _board;
    Tilemap _floor;
    private void Awake()
    {
        _dungeon = transform.gameObject;
        EnemyDic = new Dictionary<Vector3Int, EnemyData>();
        EnemyQueue = new SimplePriorityQueue<EnemyData, int>();
    }
    public void SpawnEnemy()
    {
        GameObject newEnemy;
        KeyValuePair<Vector3Int, TileInfo> pair;
        DungeonInfo dungeonInfo = _dungeon.GetComponent<DungeonInfo>();
        _board = dungeonInfo.Board;
        _floor = dungeonInfo.tilemaps[0];
        Stack<Vector3Int> randomCoorStack = new Stack<Vector3Int>();
        for (int i = 0; i < dungeonInfo.EnemyCount; i++)
        {
            newEnemy = Managers.ResourceMgr.Instantiate("Unit/Enemy", _floor.transform);
            EnemyData newEnemyData = newEnemy.GetComponent<EnemyData>();
            newEnemyData.SetDataFromLibrary(EnemyLibrary._abandonedMinshaftDex.GetUnit("Unit_Miner"));
            while(true)
            {
                pair = _board.ElementAt(Random.Range(0, _board.Count - 1));
                if(pair.Value.Occupied == Define.OccupiedType.Empty) 
                { 
                    pair.Value.Occupied = Define.OccupiedType.Enemy;
                    break; 
                }
            }
            EnemyDic.Add(pair.Key, newEnemyData);
            newEnemy.transform.position = _floor.GetCellCenterWorld(pair.Key);
            EnemyQueue.Enqueue(newEnemyData);
        }
    }
}
