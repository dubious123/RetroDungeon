using Priority_Queue;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawningPool : MonoBehaviour
{
    Dictionary<Vector3Int, EnemyData> EnemyDic;
    private class EnemyPriorityComparer : IComparer<int>
    {
        public int Compare(int xSpeed, int ySpeed)
        {
            return -Compare(xSpeed, ySpeed);
        }
    }
    SimplePriorityQueue<EnemyData, int> EnemyQueue;
    Dictionary<Vector3Int, TileInfo> _board;
    Tilemap _floor;
    private void Awake()
    {
        EnemyDic = new Dictionary<Vector3Int, EnemyData>();
        EnemyQueue = new SimplePriorityQueue<EnemyData, int>();
    }
    public void SpawnEnemy(GameObject dungeon)
    {
        
        DungeonInfo dungeonInfo = dungeon.GetComponent<DungeonInfo>();
        _board = dungeonInfo.Board;
        _floor = dungeonInfo.tilemaps[0];
        Stack<Vector3Int> randomCoorStack = new Stack<Vector3Int>();
        for (int i = 0; i < dungeonInfo.EnemyCount; i++)
        {
            EnemyData newEnemyData = Managers.ResourceMgr.Instantiate("Unit/Enemy",_floor.transform).GetComponent<EnemyData>();
            //Todo : Randomize generator
            //newEnemyData.SetDataFromLibrary(Managers.GameMgr.EnemyDex.)
            EnemyDic.Add(_board.ElementAt(Random.Range(0, _board.Count - 1)).Key, null);

        }
    }
}
