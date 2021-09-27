using Priority_Queue;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawningPool
{ 
    public static List<BaseUnitData> UnitList = new List<BaseUnitData>();
    public static void GenerateUnits(DungeonGenerationInfo dungeonInfo, Dungeon dungeon)
    {
        foreach (KeyValuePair<string,int> pair in dungeonInfo.UnitList)
        {
            GameObject unit;
            BaseUnitData unitData;
            for (int i = 0; i < pair.Value; i++)
            {
                unit = Managers.ResourceMgr.Instantiate("Unit/BaseUnit");
                unitData = unit.GetOrAddComponent<BaseUnitData>();
                UnitLibrary.SetUnitData(pair.Key, unitData);
                TileInfo tile;
                while (true)
                {
                    tile = dungeon.GetRandomTile();
                    if(tile.Unit == null)
                    {
                        UnitList.Add(unitData);
                        unitData.CurrentCellCoor = tile.Coor;
                        Managers.GameMgr.SetUnit(unit, tile.Coor);
                        break;
                    }
                }
            }

        }
    }
}
