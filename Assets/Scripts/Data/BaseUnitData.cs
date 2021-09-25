using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseUnitData : MonoBehaviour
{
    protected Tilemap _floor;
    Vector3Int _currentCellCoor = new Vector3Int(int.MinValue,int.MinValue,int.MinValue);
    protected Define.CharDir _lookDir;
    protected Define.Unit _unitType;
    Color _tileColor;
    protected string _unitName;
    
    protected Define.UnitMentalState _mental;
    protected int _maxHp;
    protected int _hp;
    protected int _maxDef;
    protected int _def;
    protected int _maxMp;
    protected int _mp;
    protected int _maxMs;
    protected int _ms;
    protected int _maxShock;
    protected int _shock;
    protected int _maxStress;
    protected int _stress;
    protected float _moveSpeed;
    protected int _maxAp;
    protected int _recoverAp;
    protected int _currentAp;
    protected int _eyeSight;

    protected List<string> _allienceList;
    protected List<string> _enemyList;
    protected Define.WeaponType _weapon;
     
    protected Dictionary<string, SkillLibrary.BaseSkill> _skillDict;


    public Vector3Int CurrentCellCoor { get { return _currentCellCoor; } set { UpdateCoor(value); } }
    public Define.CharDir LookDir { get { return _lookDir; } set { _lookDir = value; } }

    public Define.Unit UnitType { get { return _unitType; } set { _unitType = value; } }
    public Color TileColor { get { UpdateColor(); return _tileColor; } }
    public string UnitName { get { return _unitName; } set { _unitName = value; } }


    public Define.UnitMentalState Mental { get { return _mental; } set { _mental = value; } }

    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxDef { get { return _maxDef; } set { _maxDef = value; } }
    public int Def { get { return _def; } set { _def = value; } }
    public int MaxMp { get { return _maxMp; } set { _maxMp = value; } }
    public int Mp { get { return _mp; } set { _mp = value; } }
    public int MaxMS { get { return _maxMs; } set { _maxMs = value; } }
    public int MS { get { return _ms; } set { _ms = value; } }
    public int MaxShock { get { return _maxShock; } set { _maxShock = value; } }
    public int Shock { get { return _shock; } set { _shock = value; } }
    public int MaxStress { get { return _maxStress; } set { _maxStress = value; } }
    public int Stress { get { return _stress; } set { _stress = value; } }


    public float Movespeed { get { return _moveSpeed; } }
    public int MaxAp { get { return _maxAp; } }
    public int RecoverAp { get { return _recoverAp; } }
    public int CurrentAp { get { return _currentAp; } set { _currentAp = value; } }
    public int EyeSight { get { return _eyeSight; } set { _eyeSight = value; } }
    public Define.WeaponType Weapon { get { return _weapon; } }

    public List<string> AllienceList { get { return _allienceList; } }
    public List<string> EnemyList { get { return _enemyList; } }

    public Dictionary<string, SkillLibrary.BaseSkill> SkillDict { get { return _skillDict; } }

    void Awake()
    {
        Init();
    }
    public virtual void Init()
    {
        _floor = Managers.GameMgr.Floor;
        _skillDict = new Dictionary<string, SkillLibrary.BaseSkill>();
    }
    protected virtual void UpdateCoor(Vector3Int newPos)
    {
        Dictionary<Vector3Int, TileInfo> board = Managers.DungeonMgr.GetTileInfoDict(Managers.DungeonMgr.Level);
        if (board.ContainsKey(_currentCellCoor)) { board[_currentCellCoor].RemoveUnit(); }
        board[newPos].SetUnit(gameObject);
        Managers.UI_Mgr.MoveTileSet(Define.TileOverlay.Unit, _currentCellCoor, newPos, TileColor);
        _currentCellCoor = newPos;
    }
    internal int UpdateApCost(int cost)
    {
        _currentAp -= cost;
        if (_currentAp > _maxAp) { _currentAp = _maxAp; }
        else if (_currentAp < 0) { _currentAp = 0; }
        return _currentAp;
    }
    public int UpdateApRecover(int recover)
    {
        return UpdateApCost(-recover);
    }
    public virtual bool IsDead() 
    {
        return _hp <= 0;
    }
    public virtual IEnumerator<float> _PerformDeath()
    {
        Managers.DungeonMgr.GetTileInfoDict()[CurrentCellCoor].RemoveUnit();
        Managers.UI_Mgr.RemoveTileSet(Define.TileOverlay.Unit, CurrentCellCoor);
        Managers.UI_Mgr.ShowOverlay();
        yield break;
    }
    public virtual void Response()
    {
        GetComponent<AnimationController>()._PlayAnimation("hit1", 1).RunCoroutine(); 
    }
    protected virtual void UpdateColor()
    {
        //Todo
        if(_unitName == "Player") { _tileColor = Color.green; }
        else if (EnemyList.Contains("Player")) { _tileColor = Color.red; }
        else if (AllienceList.Contains("Player")) { _tileColor = Color.green; }

    }
}
