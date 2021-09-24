using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEvents : MonoBehaviour
{
    GameObject _nextDungeonText = GameObject.Find("ToTheNextDungeon"); 
    public void EnableExit(GameObject unit)
    {
        if(unit.GetComponent<BaseUnitData>().UnitName != "Player") { return; }
        _nextDungeonText.SetActive(true);
    }
}
