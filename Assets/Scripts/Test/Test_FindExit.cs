using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_FindExit : MonoBehaviour
{
    public void FindExit()
    {
        Managers.CameraMgr.GameCamController.TargetPos = 
            Managers.GameMgr.Floor.GetCellCenterWorld
            (Managers.DungeonMgr.CurrentDungeon.GetComponent<DungeonGenerationInfo>().ExitCoor);
    }
}
