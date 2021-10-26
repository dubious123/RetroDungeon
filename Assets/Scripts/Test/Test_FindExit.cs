using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_FindExit : MonoBehaviour
{
    public void FindExit()
    {
        Managers.CameraMgr.GameCamController.TargetPos =
            Managers.GameMgr.Floor.GetCellCenterWorld
            (Managers.GameMgr.CurrentDungeon.ExitPosList[0]);
    }
}
