using Priority_Queue;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawningPool : MonoBehaviour
{ 
    public Dictionary<WorldPosition,List<BaseUnitData>> UnitDict = new Dictionary<WorldPosition, List<BaseUnitData>>();
    public void GenerateUnits(DungeonGenerationInfo dungeonInfo, Dungeon dungeon)
    {
        WorldPosition worldPos = dungeonInfo.ID;
        string worldName = worldPos.World.ToString();
        if (!UnitDict.ContainsKey(worldPos)) { UnitDict.Add(worldPos, new List<BaseUnitData>()); }
        Transform world = transform.Find(worldName);
        if(world == null) { world = new GameObject($"{worldName}").transform; world.SetParent(gameObject.transform); }
        Transform level = world.Find($"Level : {worldPos.Level}");
        if(level == null) { level = new GameObject($"Level : {worldPos.Level}").transform; level.SetParent(world); }

        foreach (KeyValuePair<string,int> pair in dungeonInfo.UnitList)
        {
            GameObject unit;    
            BaseUnitData unitData;
            for (int i = 0; i < pair.Value; i++)
            {
                unit = Managers.ResourceMgr.Instantiate("Unit/BaseUnit",level);
                unitData = unit.GetOrAddComponent<BaseUnitData>();
                UnitLibrary.SetUnitData(pair.Key, unitData);
                TileInfo tile;
                while (true)
                {
                    tile = dungeon.GetRandomTile();
                    if(tile.Type!=Define.TileType.Empty&&!Managers.GameMgr.IsTileOccupied(tile.Coor))
                    {
                        UnitDict[worldPos].Add(unitData);
                        unitData.CurrentCellCoor = tile.Coor;
                        Managers.GameMgr.MoveUnit(unit, tile.Coor);
                        Managers.GameMgr.SetUnit(unit, tile.Coor);
                        break;
                    }
                }
            }

        }
    }
}
