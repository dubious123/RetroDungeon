using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    /// <summary>
    /// 0 : Body
    /// 1 : WeaponCover
    /// 2 : Weapon
    /// 3 : WeaponFlash
    /// </summary>
    Animator[] _animators;
    Animator _bodyAnim;
    Animator _weaponCoverAnim;
    Animator _weaponAnim;
    Animator _weaponFlashAnim;

    Define.WeaponType _weaponType;
    bool _hasWeapon;
    string _dir;

    void Awake()
    {
        Init();
    }
    void Init()
    {
        SetAnimators();
    }
    private void SetAnimators()
    {
        _animators = gameObject.GetComponentsInChildren<Animator>(true);
        _bodyAnim = _animators[0]; 
        _weaponCoverAnim = _animators[1];
        _weaponAnim = _animators[2];
        _weaponFlashAnim = _animators[3];
    }

    public void PlayAnimation(string animName)
    {
        UpdateDir();
        CheckWeapon(); //Todo
        RunAnimators(animName);

    }
    private void UpdateDir()
    {
        switch (Managers.GameMgr.Player_Data.LookDir)
        {
            case Define.CharDir.Up:
                _dir = "up";
                break;
            case Define.CharDir.Right:
                FlipSpriteRight();
                _dir = "side";
                break;
            case Define.CharDir.Down:
                _dir = "down";
                break;
            case Define.CharDir.Left:
                FlipSpriteLeft();
                _dir = "side";
                break;
        }
    }
    private void FlipSpriteRight()
    {
        foreach(SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.flipX = true;
        }
    }
    private void FlipSpriteLeft()
    {
        foreach (SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.flipX = false;
        }
    }
    private void CheckWeapon()
    {
        _weaponType = Managers.GameMgr.Player_Data.Weapon;
        _hasWeapon = _weaponType != Define.WeaponType.None;
        EnableWeaponAnim(_hasWeapon);
    }
    private void EnableWeaponAnim(bool activate)
    {
        if(activate == true)
        {
            foreach (Animator animator in _animators)
            {
                animator.gameObject.SetActive(activate);
            }
        }
        else
        {
            for (int i = 1; i < _animators.Length; i++)
            {
                _animators[i].gameObject.SetActive(activate);
            }
        }

    }

    void RunAnimators(string animName)
    {
        if (_hasWeapon)
        {
            //Todo
            _bodyAnim.CrossFade($"Player.{animName}_{_dir}",0.1f);
            _weaponCoverAnim.CrossFade($"Player.{animName}_{_dir}", 0.1f);
            _weaponAnim.CrossFade($"Player.{animName}_{_dir}", 0.1f);
            _weaponFlashAnim.CrossFade($"Player.{animName}_{_dir}", 0.1f);
        }
        else
        {
            _bodyAnim.CrossFade($"Player.{animName}_{_dir}", 0.1f);
        }
    }
    
}
