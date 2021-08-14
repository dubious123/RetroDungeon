using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseUnitData : MonoBehaviour
{
    protected Tilemap _floor;
    protected Vector3Int _currentCellCoor;
    protected Define.CharDir _lookDir;
    protected Define.Unit _unitType;
    protected string _unitName;
    
    protected Define.UnitMentalState _mental;
    protected int _maxHp;
    protected int _hp;
    protected float _moveSpeed;
    protected int _maxAp;
    protected int _recoverAp;
    protected int _currentAp;
    protected int _eyeSight;

    protected List<string> _allienceList;
    protected List<string> _enemyList;
    protected Define.WeaponType _weapon;

    protected List<string> _skillList;


    public Vector3Int CurrentCellCoor { get { return _currentCellCoor; } set { _currentCellCoor = value; } }
    public Define.CharDir LookDir { get { return _lookDir; } set { _lookDir = value; } }

    public Define.Unit UnitType { get { return _unitType; } set { _unitType = value; } }
    public string UnitName { get { return _unitName; } set { _unitName = value; } }


    public Define.UnitMentalState Mental { get { return _mental; } set { _mental = value; } }
    public int MaxHp { get { return _maxHp; } }
    public int Hp { get { return _hp; } }
    public float Movespeed { get { return _moveSpeed; } }
    public int MaxAp { get { return _maxAp; } }
    public int RecoverAp { get { return _recoverAp; } }
    public int CurrentAp { get { return _currentAp; } set { _currentAp = value; } }
    public int EyeSight { get { return _eyeSight; } set { _eyeSight = value; } }
    public Define.WeaponType Weapon { get { return _weapon; } }

    public List<string> AllienceList { get { return _allienceList; } }
    public List<string> EnemyList { get { return _enemyList; } }

    public List<string> SkillList { get { return _skillList; } }

    void Awake()
    {
        Init();
    }
    public virtual void Init()
    {
        _floor = Managers.GameMgr.Floor;
    }

    internal int UpdateAp(int cost)
    {
        _currentAp += cost;
        if (_currentAp > _maxAp) { _currentAp = _maxAp; }
        else if (_currentAp < 0) { _currentAp = 0; }
        return _currentAp;
    }
}
