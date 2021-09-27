using Priority_Queue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManagerEx
{
    WorldPosition _currentWorld = new WorldPosition(Define.World.AbandonedMineShaft, 1);
    GameObject _player;
    PlayerController _playerController;
    PlayerData _playerData;
    Tilemap _floor;
    public WorldPosition CurrentWorld { get { return _currentWorld; } }
    public Dungeon CurrentDungeon { get { return Managers.WorldMgr.WorldMap[_currentWorld]; } }
    public GameObject Player { get { return _player; } }
    public PlayerController Player_Controller { get { return _playerController; } }
    public PlayerData Player_Data { get { return _playerData; } }
    public Tilemap Floor 
    {
        get
        {
            if(_floor == null)
            {
                _floor = Managers.WorldMgr.World_go.GetComponentsInChildren<Tilemap>()[0];
            }
            return _floor;
        }
    }

    public void Init()
    {
    }
    public void CreatePlayer()
    {
        _player = Managers.ResourceMgr.Instantiate("Player/Player", _floor.transform);
        _playerController = _player.GetComponent<PlayerController>();
        _playerData = _player.GetOrAddComponent<PlayerData>();
        _playerData.CurrentCellCoor = Vector3Int.zero;
        SetUnit(_player, Vector3Int.zero);
    }
    public void StartGame()
    {
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

    public void EnterTheDungeon()
    {

    }
    public void FallToTheDungeion()
    {

    }
    public void Clear()
    {
        _currentWorld = null;
    }
    public void SetUnit(GameObject unit, Vector3Int pos)
    {
        unit.transform.position = Floor.GetCellCenterWorld(Vector3Int.zero);
    }
    public void MoveUnit(GameObject unit, Vector3Int newPos)
    {
        MoveUnit(unit.GetComponent<BaseUnitData>(), newPos);
    }
    public void MoveUnit(BaseUnitData unit, Vector3Int newPos)
    {
        if (Managers.GameMgr.HasTile(unit.CurrentCellCoor))
        {
            RemoveUnit(unit.CurrentCellCoor);
            CurrentDungeon.GetTile(newPos).SetUnit(unit.gameObject);
            Managers.UI_Mgr.MoveTileSet(Define.TileOverlay.Unit, unit.CurrentCellCoor, newPos, unit.TileColor);
        }      
        unit.CurrentCellCoor = newPos;
    }
    public void RemoveUnit(Vector3Int pos)
    {
        CurrentDungeon.GetTile(pos).RemoveUnit();
        Managers.UI_Mgr.RemoveTileSet(Define.TileOverlay.Unit, pos);
        Managers.UI_Mgr.ShowOverlay();
    }
    public bool IsTileOccupied(Vector3Int pos)
    {
        return CurrentDungeon.GetTile(pos).Unit == null;
    }
    public GameObject GetUnit(Vector3Int pos)
    {
        return CurrentDungeon.GetTile(pos).Unit;
    }
    public BaseUnitData GetUnitData(Vector3Int pos)
    {
        return CurrentDungeon.GetTile(pos).Unit?.GetComponent<BaseUnitData>();
    }
    public bool IsTileEmpty(Vector3Int pos)
    {
        return CurrentDungeon.IsEmpty(pos);
    }
    public bool HasTile(Vector3Int pos)
    {
        return CurrentDungeon.IsInBound(pos);
    }
}
