using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    void Awake()
    {
        Init();
    }
    public virtual void Init()
    {
        _skillDict = new Dictionary<string, BaseSkill>();
        _itemDict = new Dictionary<string, BaseItem>();
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
        Managers.UI_Mgr.DownPanel.PutSkill(skill.Name);
    }
    public virtual void PutPocket(BaseItem item)
    {
        if (!_itemDict.ContainsKey(item.ItemName)) 
        { 
            _itemDict.Add(item.ItemName,item);
            Managers.UI_Mgr.DownPanel.PutSkill(item.ItemName);
        }
        _itemDict[item.ItemName].CurrentStack++;
    }
}
