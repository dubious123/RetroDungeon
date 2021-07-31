using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    public Define.World _currentWorld { get; private set; } = Define.World.Unknown;

    GameObject _player;
    PlayerData _playerData;
    Transform[] _tilemaps;
    
    public Transform[] Tilemaps { get { return _tilemaps; } }

    public PlayerData Player { get { return _playerData; } }
    public PlayerData PlayerData { get { return _playerData; } }

    public void Init()
    {
        _currentWorld = Define.World.AbandonedMineShaft;
    }

    public GameObject Spawn(Define.WorldObject type, string path, GameObject dungeon)
    {
        _tilemaps = dungeon.GetChildren();
        GameObject go = Managers.Resource.Instantiate(path, _tilemaps[0]);
        switch (type)
        {
            case Define.WorldObject.Player:
                _player = go;
                _playerData = new PlayerData(_player, _tilemaps[0]);
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
