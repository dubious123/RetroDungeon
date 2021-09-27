using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseUnitData : MonoBehaviour
{
    public BaseUnitStat Stat { get; set; }
    public Define.Unit UnitType { get; protected set; }
    public string UnitName { get; set; }


    public Define.UnitMentalState Mental { get; set; } = Define.UnitMentalState.Hostile;
    public Define.WeaponType Weapon { get; set; } = Define.WeaponType.None;

    public List<string> EnemyList { get; set; } = new List<string>();
    public List<string> AllienceList { get; set; } = new List<string>();
    public List<string> SkillList { get; set; } = new List<string>();

    Vector3Int _currentCellCoor = new Vector3Int(int.MinValue,int.MinValue,int.MinValue);
    Color _tileColor;


    protected Dictionary<string, SkillLibrary.BaseSkill> _skillDict;


    public Vector3Int CurrentCellCoor { get { return _currentCellCoor; } set { Managers.GameMgr.MoveUnit(this,value); } }
    public Define.CharDir LookDir { get; set; }

    public Color TileColor { get { UpdateColor(); return _tileColor; } }




    public Dictionary<string, SkillLibrary.BaseSkill> SkillDict { get { return _skillDict; } }

    void Awake()
    {
        Init();
    }
    public virtual void Init()
    {
        _skillDict = new Dictionary<string, SkillLibrary.BaseSkill>();
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
        yield break;
    }
    public virtual void Response()
    {
        GetComponent<AnimationController>()._PlayAnimation("hit1", 1).RunCoroutine(); 
    }
    protected virtual void UpdateColor()
    {
        if(UnitName == "Player") { _tileColor = Color.green; }
        else if (EnemyList.Contains("Player")) { _tileColor = Color.red; }
        else if (AllienceList.Contains("Player")) { _tileColor = Color.green; }
    }
}
