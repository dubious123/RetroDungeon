using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    Vector3Int _targetPos;
    BaseUnitData _targetUnit;
    List<BaseUnitData> _unitsInRange;
    SkillLibrary.BaseSkill _currentSkill;
    public void SkillFromTo(BaseUnitData from, Vector3Int targetPos, SkillLibrary.BaseSkill skill)
    {
        _targetPos = targetPos;
        _currentSkill = skill;
        _targetUnit = Managers.DungeonMgr.GetTileInfoDict()[targetPos].Unit.GetComponent<BaseUnitData>();
        if (_targetUnit == null) { Debug.Log("NoTarget"); return; }
        _unitsInRange.Clear();
        GetUnitsInRange();
        DealDamage();
        from.UpdateAp(skill.Cost);
    }
    private void GetUnitsInRange()
    {
        foreach(Vector3Int delta in _currentSkill.Area)
        {
            _unitsInRange.Add(Managers.DungeonMgr.GetTileInfoDict()[_targetPos + delta].Unit?.GetComponent<BaseUnitData>());
        }
    }
    private void DealDamage()
    {
        foreach(BaseUnitData target in _unitsInRange)
        {
            
        }
    }
}
