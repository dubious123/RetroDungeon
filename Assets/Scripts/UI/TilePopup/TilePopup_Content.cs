using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePopup_Content : MonoBehaviour
{
    [SerializeField] GameObject _Unit;
    [SerializeField] UnitStatus _Status;
    [SerializeField] TilePopup_UnitDetail _UnitDetail;
    [SerializeField] TilePopup_UnitSkill _UnitSkill;
    [SerializeField] TilePopup_TileInfo _TileInfo;
    public void Init(Vector3Int pos, GameObject unit)
    {
        if(unit == null) { _Unit.SetActive(false); } 
        else
        {
            BaseUnitData data = unit.GetComponent<BaseUnitData>();
            _Unit.SetActive(true); _Status.Init(data);
            _UnitDetail.Init(data);
            _UnitSkill.Init(data);
        }
        _TileInfo.Init(Managers.DungeonMgr.GetTileInfoDict()[pos]);
    }
}
