using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    PlayerData _data;
    RectTransform _hp;
    RectTransform _hpGauge;
    RectTransform _def;
    RectTransform _defGauge;
    RectTransform _mp;
    RectTransform _mpGauge;
    RectTransform _ms;
    RectTransform _msGauge;
    RectTransform _shock;
    RectTransform _shockGauge;
    RectTransform _stress;
    RectTransform _stressGauge;

    int _memorizedMaxHp;
    int _memorizedHp;
    int _memorizedMaxDef;
    int _memorizedDef;
    int _memorizedMaxMp;
    int _memorizedMp;
    int _memorizedMaxMs;
    int _memorizedMs;
    int _memorizedMaxShock;
    int _memorizedShock;
    int _memorizedMaxStress;
    int _memorizedStress;

    public void Init(PlayerData playerData)
    {
        Transform[] children = gameObject.GetChildren();
        Transform[] GrandChild = children[0].gameObject.GetChildren();
        _hp = GrandChild[0].GetComponent<RectTransform>();
        _hpGauge = _hp.GetChild(0).GetComponent<RectTransform>();
        _def = GrandChild[1].GetComponent<RectTransform>();
        _defGauge = _def.GetChild(0).GetComponent<RectTransform>();

        GrandChild = children[1].gameObject.GetChildren();
        _mp = GrandChild[0].GetComponent<RectTransform>();
        _mpGauge = _mp.GetChild(0).GetComponent<RectTransform>();
        _ms = GrandChild[1].GetComponent<RectTransform>();
        _msGauge = _ms.GetChild(0).GetComponent<RectTransform>();

        _shock = children[2].GetChild(0).GetComponent<RectTransform>();
        _shockGauge = _shock.GetChild(0).GetComponent<RectTransform>();

        _stress = children[3].GetChild(0).GetComponent<RectTransform>();
        _stressGauge = _stress.GetChild(0).GetComponent<RectTransform>();

        _data = playerData;
        _memorizedMaxHp = int.MinValue;
        _memorizedHp = int.MinValue;
        _memorizedMaxDef = int.MinValue;
        _memorizedDef = int.MinValue;
        _memorizedMaxMp = int.MinValue;
        _memorizedMp = int.MinValue;
        _memorizedMaxMs = int.MinValue;
        _memorizedMs = int.MinValue;
        _memorizedMaxShock = int.MinValue;
        _memorizedShock = int.MinValue;
        _memorizedMaxStress = int.MinValue;
        _memorizedStress = int.MinValue;
        InvokeRepeating("UpdatePlayerStatusBar", 1.0f, 0.5f);
    }
    void UpdatePlayerStatusBar()
    {
        if (_memorizedMaxHp != _data.MaxHp) 
        { 
            UpdateMaxAnchor(_hp, ref _memorizedMaxHp, _data.MaxHp);
            UpdateLeftBarAnchor(_def, _hp);
        }
        if (_memorizedHp != _data.Hp) { UpdateGaugeAnchor(_hpGauge, ref _memorizedHp, _memorizedMaxHp, _data.Hp); }
        if (_memorizedMaxDef != _data.MaxDef) { UpdateMaxAnchor(_def, ref _memorizedMaxDef, _data.MaxDef); }
        if (_memorizedDef != _data.Def) { UpdateGaugeAnchor(_defGauge, ref _memorizedDef, _memorizedMaxDef, _data.Def); }
        if (_memorizedMaxMp != _data.MaxMp) 
        { 
            UpdateMaxAnchor(_mp, ref _memorizedMaxMp, _data.MaxMp);
            UpdateLeftBarAnchor(_ms, _mp);
        }
        if (_memorizedMp != _data.Mp) { UpdateGaugeAnchor(_mpGauge, ref _memorizedMp, _memorizedMaxMp, _data.Mp); }
        if (_memorizedMaxMs != _data.MaxMS) { UpdateMaxAnchor(_ms, ref _memorizedMaxMs, _data.MaxMS); }
        if (_memorizedMs != _data.MS) { UpdateGaugeAnchor(_msGauge, ref _memorizedMs, _memorizedMaxMs, _data.MS); }
        if (_memorizedMaxShock != _data.MaxShock) { UpdateMaxAnchor(_shock, ref _memorizedMaxShock, _data.MaxShock); }
        if (_memorizedShock != _data.Shock) { UpdateGaugeAnchor(_shockGauge, ref _memorizedShock, _memorizedMaxShock, _data.Shock); }
        if (_memorizedMaxStress != _data.MaxStress) { UpdateMaxAnchor(_stress, ref _memorizedMaxStress, _data.MaxStress); }
        if (_memorizedStress != _data.Stress) { UpdateGaugeAnchor(_stressGauge, ref _memorizedStress, _memorizedMaxStress, _data.Stress); }
    }

    void UpdateMaxAnchor(RectTransform rect, ref int memorizedData ,int newData)
    {
        memorizedData = newData;
        Vector2 delta = new Vector2(0.2f, 0f);
        rect.anchorMin = rect.anchorMax - delta;
    }
    void UpdateLeftBarAnchor(RectTransform left, RectTransform right)
    {
        Vector2 diff = right.anchorMin - left.anchorMax;
        left.anchorMax += diff;
        left.anchorMin += diff;
    }
    void UpdateGaugeAnchor(RectTransform rect, ref int memorizedData,int maxData ,int newData)
    {
        memorizedData = newData;
        rect.anchorMin = new Vector2(1 - memorizedData / (float)maxData, 0);
    }
}
