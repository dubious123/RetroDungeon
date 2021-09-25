using Priority_Queue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManagerEx
{
    Define.World _currentWorld;
    GameObject _player;
    PlayerController _playerController;
    PlayerData _playerData;
    Transform[] _gridLayers;
    Tilemap[] _tilemaps;
    Tilemap _floor;

    public Define.World CurrentWorld { get { return _currentWorld; } }
    public GameObject Player { get { return _player; } }
    public PlayerController Player_Controller { get { return _playerController; } }
    public PlayerData Player_Data { get { return _playerData; } }
    public Transform[] Tilemaps { get { return _gridLayers; } }
    public Tilemap Floor { get { return _floor; } }

    public void Init()
    {
        _currentWorld = Define.World.AbandonedMineShaft;
    }

    public void StartGame()
    {
        Managers.DungeonMgr.CurrentDungeon.GetComponent<SpawningPool>().SpawnUnits();
        Managers.InputMgr.InitControllers(_player);
        _floor.GetComponent<Imouse>().Init();
        Managers.UI_Mgr.InitPlayerStatusBar(_playerData);
        Managers.UI_Mgr.SetFloorOverlay();
        Managers.TurnMgr.UpdateDataFromCurrentSpawningPool();
        Managers.TurnMgr.HandlePlayerTurn();
    }
    public void PerformPlayerLose()
    {
        Managers.ResourceMgr.Destroy(_player);
        Managers.ResourceMgr.Instantiate("UI/Popup/EndingPopup_PlayerDeath");
        
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
        _gridLayers = dungeonInfo.GridLayers;
        _tilemaps = dungeonInfo.Tilemaps;
        _floor = _tilemaps[0];
        _player = Managers.ResourceMgr.Instantiate("Player/Player", _gridLayers[0]);
        _playerController = _player.GetComponent<PlayerController>();
        _playerData = _player.GetOrAddComponent<PlayerData>();
        _playerData.CurrentCellCoor = Vector3Int.zero;
        _player.transform.position = _floor.GetCellCenterWorld(Vector3Int.zero);
        return _player;
    }
    public void ToTheNextDungeon()
    {
    }
    public void Clear()
    {
        _currentWorld = Define.World.Unknown;
    }


}
