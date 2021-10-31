using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    Vector3Int _targetPos;
    BaseUnitStat _unitStat;
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
        _unitStat = from.Stat;
        if (_targetUnit == null) { Debug.Log("NoTarget"); return; }
        from.UpdateApCost(skill.Cost);
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
        int pd = _currentSkill.AttackDamage * (100 + _unitStat.AttackDamage_Percentage) / 100 + _unitStat.AttackDamage;
        target.Stat.Def -= pd;
        if(target.Stat.Def < 0) 
        { 
            target.Stat.Hp += target.Stat.Def;
            target.Stat.Def = 0;
        }
    }
    private void DealMagicDamage(BaseUnitData target)
    {
        int md = _currentSkill.MagicDamage * (100 + _unitStat.MagicDamage_Percentage) / 100 + _unitStat.MagicDamage;
        target.Stat.Ms -= md;
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
        int md = _currentSkill.MentalDamage * (100 + _unitStat.MentalDamage_Percentage) / 100 + _unitStat.MentalDamage;
        target.Mental += md;
        if(target.Mental < 0) { target.Mental = 0; }
    }
    private void DealShockDamage(BaseUnitData target)
    {
        int sd = _currentSkill.MentalDamage * (100 + _unitStat.MentalDamage_Percentage) / 100 + _unitStat.MentalDamage;
        target.Stat.Shock += sd;
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
