using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEvents
{
    static GameObject _nextDungeonText = GameObject.Find("Canvas_Game").transform.Find("ToTheNextDungeon").gameObject;
    public static void EnableExit(GameObject unit)
    {
        if(unit.GetComponent<BaseUnitData>().UnitName != "Player") { return; }
        _nextDungeonText.SetActive(true);
        Managers.InputMgr.GameController.InteractionEvent.AddListener(Managers.GameMgr.EnterTheDungeon);
    }
    public static void DisableExit(GameObject unit)
    {
        if (unit.GetComponent<BaseUnitData>().UnitName != "Player") { return; }
        _nextDungeonText.SetActive(false);
        Managers.InputMgr.GameController.InteractionEvent.RemoveListener(Managers.GameMgr.EnterTheDungeon);
    }
}
