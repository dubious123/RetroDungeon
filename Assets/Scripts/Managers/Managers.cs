using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance ;
    public static Managers Instance { get { Init(); return _instance; } }

    #region Contents
    GameManagerEx _game = new GameManagerEx();
    public static GameManagerEx Game { get { return Instance._game; } }
    #endregion


    DungeonManager _dungeon = new DungeonManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();

    public static DungeonManager Dungion { get { return Instance._dungeon; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    static void Init()
    {
        if(_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();
            _instance._pool.Init();

            _instance._game.Init();
        }
    }
    public static void Clear()
    {
        Pool.Clear();
    }
}
