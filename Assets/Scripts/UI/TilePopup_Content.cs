using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePopup_Content : MonoBehaviour
{
    [SerializeField] GameObject _Unit;
    [SerializeField] GameObject _Tile;
    [SerializeField] UnitStatus _Status;
    public void Init(GameObject unit)
    {
        if(unit == null) { _Unit.SetActive(false); }
        else { _Unit.SetActive(true); _Status.Init(unit.GetComponent<BaseUnitData>()); }
    }
}
