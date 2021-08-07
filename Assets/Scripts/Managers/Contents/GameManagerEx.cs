using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManagerEx
{
    Define.World _currentWorld;
    GameObject _player;
    PlayerData _playerData;
    Dictionary<Vector3Int, GameObject> _enemeyDic;
    Transform[] _GridLayers;
    Tilemap[] _tilemaps;
    Tilemap _floor;

    public Define.World CurrentWorld { get { return _currentWorld; } }
    public GameObject Player { get { return _player; } }
    public PlayerData Player_Data { get { return _playerData; } }
    public Dictionary<Vector3Int, GameObject> EnemyDic { get { return _enemeyDic; } }
    public Transform[] Tilemaps { get { return _GridLayers; } }
    public Tilemap Floor { get { return _floor; } }

    public void Init()
    {
        _currentWorld = Define.World.AbandonedMineShaft;
    }

    public void StartGame()
    {
        Managers.CameraMgr.InitGameCamera(_player);
        Managers.TurnMgr.UpdateTurn(Define.Turn.Player);
    }
    public GameObject Spawn(Define.WorldObject type, string path, GameObject dungeon)
    {
        //Todo
        DungeonInfo dungeonInfo = dungeon.GetComponent<DungeonInfo>();
        _GridLayers = dungeonInfo.GridLayers;
        _tilemaps = dungeonInfo.tilemaps;
        _floor = _tilemaps[0];
        GameObject go = Managers.ResourceMgr.Instantiate(path, _GridLayers[0]);
        switch (type)
        {
            case Define.WorldObject.Player:
                _player = go;
                _playerData = go.AddComponent<PlayerData>();
                break;
            case Define.WorldObject.Monster:
                break;
            case Define.WorldObject.Boss:
                break;
            default:
                break;
        }
        return go;
    }
    


}
