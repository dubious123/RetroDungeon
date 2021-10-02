using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    Vector3Int _targetPos;
    BaseUnitData _targetUnit;
    List<BaseUnitData> _unitsInRange;
    BaseSkill _currentSkill;
    public void Init()
    {
        _unitsInRange = new List<BaseUnitData>();
    }
    public void SkillFromTo(BaseUnitData from, Vector3Int targetPos, BaseSkill skill)
    {
        _targetPos = targetPos;
        _currentSkill = skill;
        _targetUnit = Managers.GameMgr.GetUnitData(targetPos);
        from.UpdateApCost(skill.Cost);
        if (_targetUnit == null) { Debug.Log("NoTarget"); return; }
        _unitsInRange.Clear();
        GetUnitsInArea();
        DealDamage();
        
    }
    private void GetUnitsInArea()
    {
        foreach(Vector3Int delta in _currentSkill.Area)
        {
            BaseUnitData unit = Managers.GameMgr.GetUnitData(_targetPos + delta);
            if(unit != null) { _unitsInRange.Add(unit); }              
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
            if (target.IsDead()) 
            {
                target._PerformDeath().CancelWith(target.gameObject).RunCoroutine();
            }
            else { target.Response(); }
        }
    }
    private void DealPhysicalDamage(BaseUnitData target)
    {
        target.Stat.Def -= _currentSkill.PhysicalDamage;
        if(target.Stat.Def < 0) 
        { 
            target.Stat.Hp += target.Stat.Def;
            target.Stat.Def = 0;
        }
    }
    private void DealMagicDamage(BaseUnitData target)
    {
        target.Stat.Ms -= _currentSkill.MagicDamage;
        if(target.Stat.Ms < 0)
        {
            target.Stat.Mp += target.Stat.Ms;
            target.Stat.Ms = 0;
            if(target.Stat.Mp < 0) { 
                target.Stat.Hp += target.Stat.Mp;
                target.Stat.Mp = 0;
            }
        }
    }
    private void DealMentalDamage(BaseUnitData target)
    {
        target.Mental += _currentSkill.MentalDamage;
        if(target.Mental < 0) { target.Mental = 0; }
    }
    private void DealShockDamage(BaseUnitData target)
    {
        target.Stat.Shock += _currentSkill.ShockDamage;
    }
    public void Clear()
    {
        _unitsInRange.Clear();
        _targetPos = Vector3Int.zero;
        _targetUnit = null;
        _unitsInRange = null;
        _currentSkill = null;
    }
}
