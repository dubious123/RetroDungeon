using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;

public class BaseUnitData : MonoBehaviour
{
    public BaseUnitStat Stat { get; set; } = new BaseUnitStat();
    public Define.Unit UnitType { get; protected set; }
    public string UnitName { get; set; }


    public Define.UnitMentalState Mental { get; set; } = Define.UnitMentalState.Hostile;
    public Define.WeaponType Weapon { get; set; } = Define.WeaponType.None;

    public List<string> EnemyList { get; set; } = new List<string>();
    public List<string> AllienceList { get; set; } = new List<string>();
    public WorldPosition WorldPos;
    Vector3Int _currentCellCoor = new Vector3Int(10000,10000,10000);
    Color _tileColor;


    protected Dictionary<string, BaseSkill> _skillDict;
    protected Dictionary<string, BaseItem> _itemDict;
    protected Dictionary<EquipmentType, BaseItem> _equipmentDict;
    public Vector3Int CurrentCellCoor
    { 
        get => _currentCellCoor;
        set
        {
            Managers.GameMgr.MoveUnit(this, value);
            _currentCellCoor = value;
        }
    }
    public Define.CharDir LookDir { get; set; }

    public Color TileColor { get { UpdateColor(); return _tileColor; } }




    public Dictionary<string, BaseSkill> SkillDict { get { return _skillDict; } }
    public Dictionary<string, BaseItem> ItemDict { get { return _itemDict; } }
    public Dictionary<EquipmentType, BaseItem> EquipmentDict { get { return _equipmentDict; } }
    void Awake()
    {
        Init();
    }
    public virtual void Init()
    {
        _skillDict = new Dictionary<string, BaseSkill>();
        _itemDict = new Dictionary<string, BaseItem>();
        _equipmentDict = new Dictionary<EquipmentType, BaseItem>();
        _equipmentDict.Add(EquipmentType.Helmet, null);
        _equipmentDict.Add(EquipmentType.Weapon, null);
        _equipmentDict.Add(EquipmentType.Armor, null);
        _equipmentDict.Add(EquipmentType.Boot, null);
        AllienceList = new List<string>();
        EnemyList = new List<string>();
        LookDir = (Define.CharDir)Random.Range(0, 4);
    }
    internal int UpdateApCost(int cost)
    {
        Stat.Ap -= cost;
        if (Stat.Ap > Stat.MaxAp) { Stat.Ap = Stat.MaxAp; }
        else if (Stat.Ap < 0) { Stat.Ap = 0; }
        return Stat.Ap;
    }
    public int UpdateApRecover(int recover)
    {
        return UpdateApCost(-recover);
    }
    public virtual bool IsDead() 
    {
        return Stat.Hp <= 0;
    }
    public virtual IEnumerator<float> _PerformDeath()
    {
        Managers.GameMgr.RemoveUnit(CurrentCellCoor);
        Managers.GameMgr.CurrentDungeon.UnitList.Remove(this);
        yield break;
    }
    public virtual void Response()
    {
        GetComponent<AnimationController>()._PlayAnimation("hit1", 1).RunCoroutine(); 
    }
    protected virtual void UpdateColor()
    {
        if(UnitName == "Player") { _tileColor = Color.green; }
        else if (AllienceList.Contains("Player")) { _tileColor = Color.green; }
        else { _tileColor = Color.red; }
    }
    public virtual void LearnSkill(BaseSkill skill)
    {
        if (_skillDict.ContainsKey(skill.Name)) { return; }
        _skillDict.Add(skill.Name, skill);
    }
    public virtual void PutPocket(BaseItem item)
    {
        if (!_itemDict.ContainsKey(item.ItemName))
        {
            _itemDict.Add(item.ItemName, item);          
            if(this is PlayerData) 
            {
                if(item.Usable) Managers.UI_Mgr.DownPanel.PutSkill(item.ItemName);
                Managers.UI_Mgr._Popup_PlayerInfo.Inventory.AddItem(item);
            }
        }
        _itemDict[item.ItemName].CurrentStack++;
    }
    public virtual void EquipItem(BaseItem item)
    {
        if (_itemDict.ContainsKey(item.ItemName) || item.Wearable)
        {
            ApplyEquipmentStat(item);
            switch (item.Equipment_Type)
            {
                case EquipmentType.Helmet:
                    _equipmentDict[EquipmentType.Helmet] = item;
                    return;
                case EquipmentType.Armor:
                    _equipmentDict[EquipmentType.Armor] = item;
                    return;
                case EquipmentType.Weapon:
                    _equipmentDict[EquipmentType.Weapon] = item;
                    return;
                case EquipmentType.Boot:
                    _equipmentDict[EquipmentType.Boot] = item;
                    return;
            }
        }
    }
    public virtual void ApplyEquipmentStat(BaseItem item)
    {
        var equipment = _equipmentDict[item.Equipment_Type];
        if (equipment != null)
        {
            Stat -= equipment.EquipmentStat;
        }
        Stat += item.EquipmentStat;
    }
}
