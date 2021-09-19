using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatus : MonoBehaviour
{
    public BaseUnitData Data { get { return _data; } }
    BaseUnitData _data;
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
    TextMeshProUGUI _hpText;
    TextMeshProUGUI _defText;
    TextMeshProUGUI _msText;
    TextMeshProUGUI _mpText;
    TextMeshProUGUI _shockText;
    TextMeshProUGUI _stressText;
    TextMeshProUGUI _name;
    int _memorizedHp;
    int _memorizedDef;
    int _memorizedMp;
    int _memorizedMs;
    int _memorizedShock;
    int _memorizedStress;
    string _memeorizedName;
    public void Init(BaseUnitData unitData)
    {
        Transform[] children = gameObject.GetChildren();
        Transform[] GrandChild = children[0].gameObject.GetChildren();
        _def = GrandChild[0].GetComponent<RectTransform>();
        _defGauge = _def.GetChild(0).GetComponent<RectTransform>();
        _defText = _def.GetChild(1).GetComponent<TextMeshProUGUI>();
        _ms = GrandChild[1].GetComponent<RectTransform>();
        _msGauge = _ms.GetChild(0).GetComponent<RectTransform>();
        _msText = _ms.GetChild(1).GetComponent<TextMeshProUGUI>();

        GrandChild = children[1].gameObject.GetChildren();
        _hp = GrandChild[0].GetComponent<RectTransform>();
        _hpGauge = _hp.GetChild(0).GetComponent<RectTransform>();
        _hpText = _hp.GetChild(1).GetComponent<TextMeshProUGUI>();
        _mp = GrandChild[1].GetComponent<RectTransform>();
        _mpGauge = _mp.GetChild(0).GetComponent<RectTransform>();
        _mpText = _mp.GetChild(1).GetComponent<TextMeshProUGUI>();

        _shock = children[2].GetChild(0).GetComponent<RectTransform>();
        _shockGauge = _shock.GetChild(0).GetComponent<RectTransform>();
        _shockText = _shock.GetChild(1).GetComponent<TextMeshProUGUI>();

        _stress = children[2].GetChild(1).GetComponent<RectTransform>();
        _stressGauge = _stress.GetChild(0).GetComponent<RectTransform>();
        _stressText = _stress.GetChild(1).GetComponent<TextMeshProUGUI>();

        _name = children[3].GetComponent<TextMeshProUGUI>();

        _data = unitData;
        _memorizedHp = int.MinValue;
        _memorizedDef = int.MinValue;
        _memorizedMp = int.MinValue;
        _memorizedMs = int.MinValue;
        _memorizedShock = int.MinValue;
        _memorizedStress = int.MinValue;
        _memeorizedName = null;
        UpdateUnitStatusBar();
        InvokeRepeating("UpdateUnitStatusBar", 1.0f, 0.5f);
    }
    void UpdateUnitStatusBar()
    {
        if (_memorizedHp != _data.Hp) { UpdateGauge(_hpGauge, ref _memorizedHp, _data.MaxHp, _data.Hp, _hpText); }
        if (_memorizedDef != _data.Def) { UpdateGauge(_defGauge, ref _memorizedDef, _data.MaxDef, _data.Def, _defText); }
        if (_memorizedMp != _data.Mp) { UpdateGauge(_mpGauge, ref _memorizedMp, _data.MaxMp, _data.Mp, _mpText); }
        if (_memorizedMs != _data.MS) { UpdateGauge(_msGauge, ref _memorizedMs, _data.MaxMS, _data.MS, _msText); }
        if (_memorizedShock != _data.Shock) { UpdateGauge(_shockGauge, ref _memorizedShock, _data.MaxShock, _data.Shock, _shockText); }
        if (_memorizedStress != _data.Stress) { UpdateGauge(_stressGauge, ref _memorizedStress, _data.MaxStress, _data.Stress, _stressText); }
        if(_memeorizedName != _data.UnitName) { _memeorizedName = _data.UnitName; _name.text = _memeorizedName;  }
    }
    void UpdateGauge(RectTransform rect, ref int memorizedData, int maxData, int newData, TextMeshProUGUI textUGUI)
    {
        memorizedData = newData;
        rect.anchorMin = new Vector2(1 - memorizedData / (float)maxData, 0);
        textUGUI.text = $"{memorizedData} / {maxData}";
    }
}
