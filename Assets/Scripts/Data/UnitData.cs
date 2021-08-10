using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitData : MonoBehaviour
{
    protected Tilemap _floor;
    protected Vector3Int _currentCellCoor;
    protected Define.CharDir _lookDir;
    protected int _maxHp;
    protected int _hp;
    protected float _moveSpeed;
    protected int _maxAp;
    protected int _recoverAp;
    protected int _currentAp;

    protected Define.WeaponType _weapon;

    public Vector3Int CurrentCellCoor { get { return _currentCellCoor; } }
    public Define.CharDir LookDir { get { return _lookDir; } set { _lookDir = value; } }
    public int MaxHp { get { return _maxHp; } }
    public int Hp { get { return _hp; } }
    public float Movespeed { get { return _moveSpeed; } }
    public int MaxAp { get { return _maxAp; } }
    public int RecoverAp { get { return _recoverAp; } }
    public int CurrentAp { get { return _currentAp; } set { _currentAp = value; } }
    public Define.WeaponType Weapon { get { return _weapon; } }


    void Awake()
    {
        Init();
    }
    public virtual void Init()
    {
        _floor = Managers.GameMgr.Floor;
    }
}
