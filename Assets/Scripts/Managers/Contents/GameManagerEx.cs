using Priority_Queue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManagerEx
{
    WorldPosition _currentWorld;
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
    public BaseUnitData Me { get; set; }
    public void Init()
    {
        _currentWorld = new WorldPosition(Define.World.AbandonedMineShaft, 1);
    }
    public void CreatePlayer()
    {
        _player = Managers.ResourceMgr.Instantiate("Player/Player", _floor.transform);
        _playerController = _player.GetComponent<PlayerController>();
        _playerData = _player.GetOrAddComponent<PlayerData>();
        _playerData.CurrentCellCoor = Vector3Int.zero;
        Me = _playerData;
        Managers.UI_Mgr._Popup_PlayerInfo.Init();
        UnitLibrary.SetUnitData("Player", _playerData);
        SetUnit(_player, Vector3Int.zero);
    }
    public void StartGame()
    {
        Managers.InputMgr.InitControllers(_player);
        _floor.GetComponent<Imouse>().Init();
        Managers.UI_Mgr.InitPlayerStatusBar(_playerData);
        Managers.UI_Mgr.ResetFloorOverlay();
        Managers.UI_Mgr.Popup_Controller.Init_GamePopups();
        EnterTheDungeon(_currentWorld);
        Managers.TurnMgr.UpdateCurrentUnitList();
        
        Managers.TurnMgr.HandlePlayerTurn();
    }
    public void PerformPlayerLose()
    {
        Managers.ResourceMgr.Destroy(_player);
        Managers.UI_Mgr.ShowPopup_PlayerDeath();   
    }
    /// <summary>
    /// First Thing GameManager Actually do
    /// bring needed data from other Component/Mgr
    /// </summary>
    /// <param name="dungeon"></param>
    /// <returns></returns>

    public void EnterTheDungeon(WorldPosition position)
    {
        RemoveUnit(_playerData.CurrentCellCoor);
        DisableUnits(_currentWorld);
        _currentWorld = position;
        _playerData.WorldPos = _currentWorld;
        Managers.UI_Mgr.UpdateTileSet(Define.TileOverlay.Unit, CurrentDungeon.GetAllUnitPos());
        DungeonRenderer.RenderDungeon(CurrentDungeon);
        EnableUnits(position);
        CurrentDungeon.GetTile(CurrentDungeon.EntrancePosList[0]).SetUnit(_player);
        //Managers.UI_Mgr.MoveTileSet(Define.TileOverlay.Unit, _playerData.CurrentCellCoor, CurrentDungeon.EntrancePosList[0], _playerData.TileColor);
        SetUnit(Player, CurrentDungeon.EntrancePosList[0]);
        _playerData.CurrentCellCoor = CurrentDungeon.EntrancePosList[0];
        Managers.UI_Mgr.ResetFloorOverlay();
        Managers.UI_Mgr.ShowGetSkillPopup();
        Managers.TurnMgr.UpdateCurrentUnitList();
        _playerController.HandleIdle();
    }
    public void EnterTheDungeon()
    {
        EnterTheDungeon(CurrentDungeon.GetTile(_playerData.CurrentCellCoor).NextWorld);     
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
        unit.transform.position = Floor.GetCellCenterWorld(pos);
    }
    public void MoveUnit(GameObject unit, Vector3Int newPos)
    {
        MoveUnit(unit.GetComponent<BaseUnitData>(), newPos);
    }
    public void MoveUnit(BaseUnitData unit, Vector3Int newPos, WorldPosition worldPos = null)
    {
        if (Managers.GameMgr.HasTile(unit.CurrentCellCoor))
        {
            if(worldPos == null) { worldPos = unit.WorldPos; }
            RemoveUnit(unit.CurrentCellCoor);
            Managers.WorldMgr.WorldMap[worldPos].GetTile(newPos).SetUnit(unit.gameObject);
            Managers.UI_Mgr.MoveTileSet(Define.TileOverlay.Unit, unit.CurrentCellCoor, newPos, unit.TileColor); 
        }      
    }
    public void RemoveUnit(Vector3Int pos)
    {
        CurrentDungeon.GetTile(pos).RemoveUnit();
        Managers.UI_Mgr.RemoveTileSet(Define.TileOverlay.Unit, pos);
        Managers.UI_Mgr.ShowOverlay();
    }
    public bool IsTileOccupied(Vector3Int pos)
    {
        return HasTile(pos) && CurrentDungeon.GetTile(pos).Unit != null;
    }
    public GameObject GetUnit(Vector3Int pos)
    {
        return CurrentDungeon.GetTile(pos).Unit;
    }
    public BaseUnitData GetUnitData(Vector3Int pos)
    {
        return CurrentDungeon.GetTile(pos).Unit?.GetComponent<BaseUnitData>();
    }
    public bool IsReachableTile(Vector3Int pos)
    {
        return HasTile(pos) && !CurrentDungeon.IsEmpty(pos);
    }
    public bool HasTile(Vector3Int pos)
    {
        return CurrentDungeon.IsInBound(pos);
    }
    public TileInfo GetTile(Vector3Int pos)
    {   
        return CurrentDungeon.GetTile(pos);
    }
    public void DisableUnits(WorldPosition pos)
    {
        foreach (BaseUnitData unit in Managers.WorldMgr.WorldMap[pos].UnitList)
        {
            unit.gameObject.SetActive(false);
        } 
    }
    public void EnableUnits(WorldPosition pos)
    {
        foreach (BaseUnitData unit in Managers.WorldMgr.WorldMap[pos].UnitList)
        {
            unit.gameObject.SetActive(true);
        }
    }
}
