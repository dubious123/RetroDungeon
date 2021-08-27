using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance ;
    public static Managers Instance { get { Init(); return _instance; } }

    #region Contents
    GameManagerEx _gameMgr = new GameManagerEx();
    public static GameManagerEx GameMgr { get { return Instance._gameMgr; } }
    #endregion

    BattleManager _battleMgr = new BattleManager();
    CameraManager _cameraMgr = new CameraManager();
    DungeonManager _dungeonMgr = new DungeonManager();
    InputManager _inputMgr = new InputManager();
    PoolManager _poolMgr = new PoolManager();
    ResourceManager _resourceMgr = new ResourceManager();
    TurnManager _turnMgr = new TurnManager();
    UI_Manager _ui_Mgr = new UI_Manager();

    
    public static BattleManager BattleMgr { get { return Instance._battleMgr; } }
    public static CameraManager CameraMgr { get { return Instance._cameraMgr; } }
    public static DungeonManager DungeonMgr { get { return Instance._dungeonMgr; } }
    public static InputManager InputMgr { get { return Instance._inputMgr; } }
    public static PoolManager PoolMgr { get { return Instance._poolMgr; } }
    public static ResourceManager ResourceMgr { get { return Instance._resourceMgr; } }
    public static TurnManager TurnMgr { get { return Instance._turnMgr; } }
    public static UI_Manager UI_Mgr { get { return Instance._ui_Mgr; } }
    private void Start()
    {
        Init();
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
            _instance._battleMgr.Init();
            _instance._cameraMgr.Init();
            _instance._dungeonMgr.Init();
            _instance._poolMgr.Init();
            _instance._gameMgr.Init();
            _instance._inputMgr.Init();
            _instance._turnMgr.Init();
            _instance._ui_Mgr.Init();
        }
    }
    public static void Clear()
    {
        PoolMgr.Clear();
    }
}
