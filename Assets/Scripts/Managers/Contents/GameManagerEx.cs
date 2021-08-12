using Priority_Queue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManagerEx
{
    Define.World _currentWorld;
    GameObject _player;
    PlayerData _playerData;
    Dictionary<GameObject, Dictionary<Vector3Int, UnitData>> _worldUnitDic;
    Transform[] _GridLayers;
    Tilemap[] _tilemaps;
    Tilemap _floor;

    public Define.World CurrentWorld { get { return _currentWorld; } }
    public GameObject Player { get { return _player; } }
    public PlayerData Player_Data { get { return _playerData; } }
    public Dictionary<GameObject, Dictionary<Vector3Int, UnitData>> WorldUnitDic { get { return _worldUnitDic; } }
    public Transform[] Tilemaps { get { return _GridLayers; } }
    public Tilemap Floor { get { return _floor; } }

    public void Init()
    {
        _currentWorld = Define.World.AbandonedMineShaft;
        _worldUnitDic = new Dictionary<GameObject, Dictionary<Vector3Int, UnitData>>();
    }

    public void StartGame()
    {
        Managers.DungeonMgr.CurrentDungeon.GetComponent<SpawningPool>().SpawnEnemy();
        Managers.InputMgr.InitControllers(_player);
        Managers.TurnMgr.UpdateDataFromCurrentSpawningPool();
        Managers.TurnMgr.UpdateTurn(Define.Turn.Player);
    }
    /// <summary>
    /// First Thing GameManager Actually do
    /// bring needed data from other Component/Mgr
    /// </summary>
    /// <param name="dungeon"></param>
    /// <returns></returns>
    public GameObject CreatePlayer(GameObject dungeon)
    {
        DungeonInfo dungeonInfo = dungeon.GetComponent<DungeonInfo>();
        _GridLayers = dungeonInfo.GridLayers;
        _tilemaps = dungeonInfo.tilemaps;
        _floor = _tilemaps[0];
        _player = Managers.ResourceMgr.Instantiate("Player/Player", _GridLayers[0]);
        _playerData = _player.GetOrAddComponent<PlayerData>();
        _player.transform.position = _floor.GetCellCenterWorld(Vector3Int.zero);
        dungeonInfo.Board[Vector3Int.zero].Occupied = Define.OccupiedType.Player;
        _worldUnitDic[dungeon].Add(Vector3Int.zero, Player_Data);
        return _player;
    }
    public Dictionary<Vector3Int, UnitData> GetEnemyDic(GameObject currentDungeon)
    {
        return _worldUnitDic[currentDungeon];
    }



}
