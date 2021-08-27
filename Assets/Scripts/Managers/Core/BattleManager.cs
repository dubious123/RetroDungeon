using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    Vector3Int _targetPos;
    BaseUnitData _targetUnit;
    List<BaseUnitData> _unitsInRange;
    SkillLibrary.BaseSkill _currentSkill;
    public void Init()
    {
        _unitsInRange = new List<BaseUnitData>();
    }
    public void SkillFromTo(BaseUnitData from, Vector3Int targetPos, SkillLibrary.BaseSkill skill)
    {
        _targetPos = targetPos;
        _currentSkill = skill;
        _targetUnit = Managers.DungeonMgr.GetTileInfoDict()[targetPos].Unit.GetComponent<BaseUnitData>();
        if (_targetUnit == null) { Debug.Log("NoTarget"); return; }
        _unitsInRange.Clear();
        GetUnitsInArea();
        DealDamage();
        from.UpdateAp(skill.Cost);
    }
    private void GetUnitsInArea()
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
            DealPhysicalDamage(target);
            DealMagicDamage(target);
            DealMentalDamage(target);
            DealShockDamage(target);
        }
    }
    private void DealPhysicalDamage(BaseUnitData target)
    {
        target.Def -= _currentSkill.PhysicalDamage;
        if(target.Def < 0) 
        { 
            target.Hp += target.Def;
            target.Def = 0;
        }
    }
    private void DealMagicDamage(BaseUnitData target)
    {
        target.MS -= _currentSkill.MagicDamage;
        if(target.MS < 0)
        {
            target.Mp += target.MS;
            target.MS = 0;
        }
    }
    private void DealMentalDamage(BaseUnitData target)
    {
        target.Mental -= _currentSkill.MentalDamage;
        if(target.Mental < 0) { target.Mental = 0; }
    }
    private void DealShockDamage(BaseUnitData target)
    {
        target.Shock -= _currentSkill.ShockDamage;
    }
}
