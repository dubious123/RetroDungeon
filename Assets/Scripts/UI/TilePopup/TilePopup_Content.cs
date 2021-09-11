using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePopup_Content : MonoBehaviour
{
    [SerializeField] GameObject _Unit;
    [SerializeField] GameObject _Tile;
    [SerializeField] UnitStatus _Status;
    [SerializeField] TilePopup_UnitDetail _UnitDetail;
    public void Init(GameObject unit)
    {
        if(unit == null) { _Unit.SetActive(false); }
        else
        {
            BaseUnitData data = unit.GetComponent<BaseUnitData>();
            _Unit.SetActive(true); _Status.Init(data);
            _UnitDetail.Init(data);
        }
    }
}
